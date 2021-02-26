var timer1 = null;

$(function () {
    
    monitorInput();
    monitorOutput();
});

window.addEventListener('message', function (event) {
    if (event.data == "startTimer") {
        if (timer1 != null) {//判断计时器是否为空
            clearInterval(timer1);
            timer1 = null;
        }

        timer1 = setInterval(monitorInput, 100);
    } else {
        if (timer1 != null) {
            clearInterval(timer1);
            timer1 = null;
        }
    }
});

window.onkeydown = function (event) {
    if (event.keyCode == 123)
        form_syn.showDebugTools();
}

function monitorInput() {
    $("#tr_input").html("");

    var inputs = io.getInputNames();
    if (inputs.length > 0) {
        var trStr = "";
        trStr += "<td style='font-weight:bold;'>传感器</td>";
        for (var i = 0; i < inputs.length; i++) {
            trStr += "<td style='text-align:center'>";
            trStr += "<div style='font-size:28px'>" + inputs[i] + "</div>";

            var res = io.getInput(inputs[i]);
            if (res == null || !res) {
                trStr += "<div class='sensor sensor-disabled' id='input_" + String(i) + "'></div>";
            }
            else {
                trStr += "<div class='sensor sensor-yellow' id='input_" + String(i) + "'></div>";
            }                
            
            trStr += "</td>";
        }
        $(trStr).appendTo("#tr_input");
    }
}

function monitorOutput() {
    $("#tr_output").html("");

    var outputs = io.getOutputNames();
    if (outputs.length > 0) {
        var trStr = "";
        trStr += "<td style='font-weight:bold;'>按钮</td>";
        for (var i = 0; i < outputs.length; i++) {
            var res = io.getOutput(outputs[i]);
            if (res == null || !res) {
                trStr += "<td><button class='rkmd-btn ripple-effect btn-grey' onclick='setValue(this)' id='output_" + String(i) + "' value='true'>" + outputs[i] + "</button></td>";
            }
            else {
                trStr += "<td><button class='rkmd-btn ripple-effect btn-green' onclick='setValue(this)' id='output_" + String(i) + "' value='false'>" + outputs[i] + "</button></td>";
            }
            
        }
        $(trStr).appendTo("#tr_output");
    }
}

function setValue(obj)
{
    var name = $(obj).text();
    var val = eval($(obj).val());
    if (val) {
        $(obj).removeClass("btn-green");
        $(obj).addClass("btn-grey");
        $(obj).val("false");
    }
    else {
        $(obj).removeClass("btn-grey");
        $(obj).addClass("btn-green");
        $(obj).val("true");
    }
    io.setOutput(name, val);
   
    monitorOutput();
}

