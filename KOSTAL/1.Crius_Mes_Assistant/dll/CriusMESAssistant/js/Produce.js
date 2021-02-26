var tmpMsg = "";
var produceID = 0;

$(function () {
    showMsg(0, "请扫描标签");

    var params = form_syn.getXmlConfigParameters();
    showParameters(params);

    var manualTreatEnable = form_syn.manualTreatEnable();
    if (!manualTreatEnable) {
        document.getElementById("btnBreakdown").disabled = true;
    }

    var bodyH = document.body.clientHeight;
    var msgH = document.getElementById('msg-header').offsetHeight;
    var tHeight = bodyH - msgH;
    document.getElementById('cus_vertical_line').style.height = (tHeight - 30).toString() + "px";
    document.getElementById('panelDiv').style.height = (tHeight - 30).toString() + "px";
    document.getElementById('rightDiv').style.height = (tHeight - 50).toString() + "px";

    var pHeight = document.getElementById('rightDiv').offsetHeight - document.getElementById('btnDiv').offsetHeight - document.getElementById('mesDiv').offsetHeight;
    document.getElementById('paramDiv').style.height = (pHeight - 30).toString() + "px";
    document.getElementById('tableDiv').style.height = (pHeight - 120).toString() + "px";
}); 

window.onkeydown = function (event) {
    if (event.keyCode == 123)
        form_syn.showDebugTools();
}

function showParameters(obj) {

    if (obj != "" && obj != "[]") {
        var data = $.parseJSON(obj);

        var tableStr = "";
        for (var i = 0; i < data.length; i++) {
            tableStr = "<tr><td valign='top' style='max-width:100px'>" + data[i].text + "：</td>";

            var val = data[i].value;
            if (val instanceof Array) {
                tableStr += "<td style='word-break : break-all;'>";
                for (var j = 0; j < val.length; j++) {
                    tableStr += val[j].text + " " + val[j].value + "<br />";
                }
                tableStr = tableStr.substring(0, tableStr.length - 1) + "</td>";
            } else {
                tableStr += "<td style='text-align:left'>" + val + "</td>";
            }
            tableStr += "</tr>";
            $(tableStr).appendTo("#paramTable");

        }
    } else {
        $("<tr><td>无相关参数</td></tr>").appendTo("#paramTable");
    }

}

function showMesSequence(data) {

    $("#panelDiv").html("");

    ////showMsg(0, "正在生产...");
    for (var m = 0; m < data.length; m++) {

        var str = "";
        str += "<div class='panel panel-default cus-panel doing' id='productDiv_" + produceID + "' name='singleProduct'>";
        str += "    <form class='form-horizontal' role='form' id='produceForm_" + produceID + "'>";
        str += "        <div class='form-group' style='text-align: right;margin-top:10px'>";
        ////str += "            <label class='control-label col-sm-1 cus-label'>变种</label>";
        ////str += "            <div class='col-sm-2'>";
        ////str += "                <div class='cus-variant-div' name='article' id='article_" + produceID + "'>" + data[m].article + "</div>";
        ////str += "            </div>";
        str += "            <label class='control-label col-sm-3 cus-label' style='text-align: center;'>条码：</label>";
        str += "            <div class='col-sm-3'>";
        str += "                <input type='text' class='form-control cus-input-readonly' name='sfc' id='sfc_" + produceID + "' value='" + data[m].barcode + "' readonly />";
        ////str += "                <div class='cus-sfc-div' name='sfc' id='sfc_" + produceID + "'>" + data[m].barcode + "</div>";
        str += "            </div>";
        str += "        </div>";
        str += "        <div class='form-group' style='margin-bottom: unset'>";
        str += "            <label class='control-label col-sm-3 cus-label' style='text-align: center;'>";
        str += "                <i aria-hidden='true' class='icon-tag cus-tag'></i>MES序列：";//<br />运行情况：";
        str += "            </label>";
        str += "            <div class='col-sm-4'>";
        str += "                <table id='mesTable_" + produceID + "' style='text-align:left;'>";
        str += "                </table>";
        str += "            </div>";
        str += "        </div>";
        str += "    </form>";
        str += "</div>";

        $(str).appendTo("#panelDiv");

        var mes = data[m].MES;
        var compResult = mes[mes.length - 1].result ? true : false;
        var isError = false;
        for (var n = 0; n < mes.length; n++) {

            if (mes[n].result || mes[n].result == null)
                isError = false;
            else {
                isError = true;
                break;
            }
        }

        if (compResult && !isError) {
            $("#productDiv_" + produceID).removeClass("doing");
            $("#productDiv_" + produceID).addClass("complete");
            //$("#sfc_" + produceID).addClass("input-complete");

            ////var str = "";
            ////for (var j = 0; j < mes.length; j = j + 2) {
            ////    str += "<tr>";
            ////    if (mes[j].result == null) {
            ////        str += "<td><img src='../img/line-w.png' /></td>";
            ////        str += "<td>" + mes[j].func + "</td>";
            ////    }
            ////    else if (mes[j].result) {
            ////        str += "<td><img src='../img/success-w.png' /></td>";
            ////        str += "<td>" + mes[j].func + "</td>";// style='color:#82e411'
            ////    }
            ////    else {
            ////        str += "<td><img src='../img/error-w.png' /></td>";
            ////        str += "<td>" + mes[j].func + "</td>";// style='color:#fb2005'
            ////    }
            ////    if (j + 1 < mes.length) {
            ////        if (mes[j + 1].result == null) {
            ////            str += "<td><img src='../img/line-w.png' /></td>";
            ////            str += "<td>" + mes[j + 1].func + "</td>";
            ////        }
            ////        else if (mes[j + 1].result) {
            ////            str += "<td><img src='../img/success-w.png' /></td>";
            ////            str += "<td>" + mes[j + 1].func + "</td>";// style='color:#82e411'
            ////        }
            ////        else {
            ////            str += "<td><img src='../img/error-w.png' /></td>";
            ////            str += "<td>" + mes[j + 1].func + "</td>";// style='color:#fb2005'
            ////        }
            ////    }
            ////    str += "</tr>";
            ////}
            ////$(str).appendTo("#mesTable_" + produceID);
        }
        else {
            if (isError) {
                $("#productDiv_" + produceID).removeClass("doing");
                $("#productDiv_" + produceID).addClass("error");
            } 
            
            ////var str = "";
            ////for (var j = 0; j < mes.length; j = j + 2) {
            ////    str += "<tr>";
            ////    if (mes[j].result == null) {
            ////        str += "<td><img src='../img/line-w.png' /></td>";
            ////        str += "<td>" + mes[j].func + "</td>";
            ////    }
            ////    else if (mes[j].result) {
            ////        str += "<td><img src='../img/success.png' /></td>";
            ////        str += "<td>" + mes[j].func + "</td>";// style='color:#82e411'
            ////    }
            ////    else {
            ////        str += "<td><img src='../img/error.png' /></td>";
            ////        str += "<td>" + mes[j].func + "</td>";// style='color:#fb2005'
            ////    }
            ////    if (j + 1 < mes.length) {
            ////        if (mes[j + 1].result == null) {
            ////            str += "<td><img src='../img/line-w.png' /></td>";
            ////            str += "<td>" + mes[j + 1].func + "</td>";
            ////        }
            ////        else if (mes[j + 1].result) {
            ////            str += "<td><img src='../img/success.png' /></td>";
            ////            str += "<td>" + mes[j + 1].func + "</td>";// style='color:#82e411'
            ////        }
            ////        else {
            ////            str += "<td><img src='../img/error.png' /></td>";
            ////            str += "<td>" + mes[j + 1].func + "</td>";// style='color:#fb2005'
            ////        }
            ////    }
            ////    str += "</tr>";
            ////}
            ////$(str).appendTo("#mesTable_" + produceID);
        }
        var str = "";
        for (var j = 0; j < mes.length; j = j + 2) {
            str += "<tr>";

            if (mes[j].result == null) {
                str += "<td><img src='../img/line-w.png' /></td>";
                str += "<td>" + mes[j].func + "</td>";
            }
            else if (mes[j].result) {
                str += "<td><img src='../img/success.png' /></td>";
                str += "<td>" + mes[j].func + "</td>";// style='color:#82e411'
            }
            else {
                str += "<td><img src='../img/error.png' /></td>";
                str += "<td>" + mes[j].func + "</td>";// style='color:#fb2005'
            }

            if (j + 1 < mes.length) {
                if (mes[j + 1].result == null) {
                    str += "<td><img src='../img/line-w.png' /></td>";
                    str += "<td>" + mes[j + 1].func + "</td>";
                }
                else if (mes[j + 1].result) {
                    str += "<td><img src='../img/success.png' /></td>";
                    str += "<td>" + mes[j + 1].func + "</td>";// style='color:#82e411'
                }
                else {
                    str += "<td><img src='../img/error.png' /></td>";
                    str += "<td>" + mes[j + 1].func + "</td>";// style='color:#fb2005'
                }
            }

            str += "</tr>";
        }
        $(str).appendTo("#mesTable_" + produceID);
        produceID++;
    }
}

function breakdown() {
    var obj = document.getElementById('btnBreakdown');

    if (obj.classList[obj.classList.length - 1] == "btn-green") {
        $("#btnBreakdown").removeClass("btn-green");
        $("#btnBreakdown").addClass("btn-red");
        $("#btnBreakdown").text("故障件");

        form_syn.manualTreatResult(true);
    } else {
        $("#btnBreakdown").removeClass("btn-red");
        $("#btnBreakdown").addClass("btn-green");
        $("#btnBreakdown").text("合格件");

        form_syn.manualTreatResult(false);
    }

    
}

//后台调用，用于显示提示信息
function showMsg(resultCode, resultText) {
    if (tmpMsg != resultText) {
        $("#msg").text("");

        var o = $("#msg")[0].classList;
        if (o.length > 2) {
            $("#msg").removeClass(o[o.length - 1]);
        }

        if (resultCode == 0) {
            $("#msg").addClass("alert-info");
            $("#msg").text(resultText);
        } else {        
			if(resultText.length > 100){
				resultText = resultText.substring(0,97) + "...";
			}
			
            $("#msg").addClass("alert-danger");
            $("#msg").text(resultText);
        }
        
    }
    tmpMsg = resultText;
}

//用于隐藏提示栏
function hideMsg() {
    $('#msg-header').empty();
}

function restoreBtn () {

    $("#btnBreakdown").removeClass("btn-red");
    $("#btnBreakdown").addClass("btn-green");
    $("#btnBreakdown").text("合格件");
    form_syn.manualTreatResult(false);
}

function cancel() {
    $("#myModal").modal('hide');
    $("#psw").val("");
    $("#error_tip").text("");
    $('.toggle').toggleClass('toggle--on')
        .toggleClass('toggle--off')
        .addClass('toggle--moving');

    setTimeout(function () {
        $('.toggle').removeClass('toggle--moving');
    }, 200)
}

function check() {
    var input = $("#psw").val();
    if (input == "Kostal8888") {
        form_syn.mesEnable(false);
        $("#myModal").modal('hide');
        $("#psw").val("");
        $("#error_tip").text("");
        
        document.getElementById("btnBreakdown").disabled = true;
    } else {
        $("#error_tip").text("密码错误");
    }
}