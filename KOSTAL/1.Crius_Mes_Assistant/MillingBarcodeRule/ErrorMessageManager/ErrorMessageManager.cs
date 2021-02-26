using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ErrorMessageManager
{
    public class ErrorMessageManager:ApplicationException
    {
        public ErrorMessageManager(Exception cException, enumExceptionType cExceptionType = enumExceptionType.Alarm)
        {
            if(cExceptionType == enumExceptionType.Alarm)
            {
                MessageBox.Show(cException.Message, "Error Alarm", MessageBoxButtons.OK);
            }else
            {
                throw cException;
            }
        }
    }

    public enum enumExceptionType
    {
        Alarm,
        Crash
    }
}
