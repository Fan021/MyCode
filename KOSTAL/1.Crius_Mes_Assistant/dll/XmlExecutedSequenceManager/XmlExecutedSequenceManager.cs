using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace MesStationCommon
{
    [Serializable]
    public class XmlExecutedSequenceManager : IExecutedSequceManager
    {
        private List<ExceutedSequeces> _results = new List<ExceutedSequeces>();


        public void addExecutedMesfunction(string sfc, string article, string  barcode, string function, bool? result)
        {
            var item = _results.FirstOrDefault(x => x.SFC == sfc);

            if(item != null)
            {
                var res = item.MES.Where(x => x.func == function).FirstOrDefault();
                if(res == null)
                {
                    item.MES.Add(new MesFunctionResult() { func = function, result = result });
                }
                else
                {
                    res.result = result;
                }
            }
            else
            {
                //no result
                _results.Add(new ExceutedSequeces()
                {
                    SFC = sfc,
                    article = article,
                    barcode = barcode,
                    MES = new List<MesFunctionResult>()
                    {
                        new MesFunctionResult()
                        {
                             func = function,
                             result = result
                        }
                    }
                });
            }
        }

        public void clear(string sfc)
        {
            _results.RemoveAll(x => x.SFC == sfc);
        }

        public void clearAll()
        {
            _results.Clear();
        }

        public List<ExceutedSequeces> getSequences(Predicate<ExceutedSequeces> predicate)
        {
            List<ExceutedSequeces> matchItems = new List<ExceutedSequeces>();

            foreach(var res in _results)
            {
                if(predicate.Invoke(res))
                {
                    matchItems.Add(res);
                }
            }

            return matchItems;
            
        }

        public ExceutedSequeces getSequences(string sfc, int MaxCount = 4)
        {
            ExceutedSequeces ret = null;
            var sequence = _results.Where(x => x.SFC == sfc).FirstOrDefault();
            
            if(sequence != null)
            {
                ret = new ExceutedSequeces() { article = sequence.article, SFC = sequence.SFC, MES = new List<MesFunctionResult>() };

                ret.MES.AddRange(sequence.MES.Take(4));
            }

            return ret;
        }

        public bool load(string file)
        {
            if(File.Exists(file))
            {
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("load executed sequences from configuration file {0}", file));

                try
                {
                    using (var r = new StreamReader(file))
                    {
                        XmlSerializer s = new XmlSerializer(typeof(List<ExceutedSequeces>));
                        var o = s.Deserialize(r) as List<ExceutedSequeces>;

                        if(o != null)
                        {
                            this._results = o;
                        }
                    }

                    return true;
                }
                catch(Exception e)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                    return false;
                }
            }

            return false;
        }

        public bool save(string file)
        {
            try
            {
                NLog.LogManager.GetCurrentClassLogger().Info(string.Format("save executed sequences to configuration file {0}", file));
                using (var r = new StreamWriter(file))
                {
                    XmlSerializer s = new XmlSerializer(typeof(List<ExceutedSequeces>));

                    s.Serialize(r, _results);
                }

                return true;
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e, e.Message);
                return false;
            }

        }
    }
}
