var paramsData = "";

$(function () {
    getParameters(); 
});

function getParameters() {
    var params = form_syn.getXmlConfigParameters();

    showParameters(params);
}

function showParameters(obj) {
    $("#paramTable").html("");

    if (obj != "" && obj != "[]") {
        var data = $.parseJSON(obj);
        paramsData = data;

        var tableStr = "";
        var tbCol = "";

        for (var i = 0; i < data.length; i++) {
            if (i != 0) {
                tableStr = "<tr id='tbCol" + String(i) + "' style='width:100%'><td>" + data[i].text + "</td>";
            } else {
                tableStr = "<tr id='tbCol" + String(i) + "' style='width:100%'><td style='width:30%'>" + data[i].text + "</td>";
            }
            tableStr += "<td style='text-align:left;'>";
            var pVal = data[i].value;
            if (pVal instanceof Array) {
                for (var j = 0; j < pVal.length; j++) {
                    tableStr += "<span title='" + pVal[j].Name + "'>" + pVal[j].text + "</span><input type='text' class='form-control custom_input' style='width:120px;' id='tbSubCol" + String(i) + "_" + String(j) + "' value='" + pVal[j].value + "' />";
                }

            } else {
                tableStr += "<input type='text' class='form-control custom_input' style='width:200px;' id='tbSubCol" + String(i) + "_0' name='" + pVal.Name + "' value='" + pVal + "' />";
            }

            tableStr += "</td></tr>";
            $(tableStr).appendTo("#paramTable");
        }

        document.getElementById('btnsDiv').style.display = "block";
    }
    else {
        $("<tr><td colspan = '2' style='text-align: left;padding-left: 20px;'>无待配置的参数</td>").appendTo("#paramTable");
        document.getElementById('btnsDiv').style.display = "none";
    }
}

function modify() {
    var inputData = paramsData;
    
    for (var i = 0; i < paramsData.length; i++) {
        var tr = $("#paramTable").find("tr[id='tbCol" + String(i) + "']");
        var tds = tr.find("td");

        var item = tds[0].innerText;
        var spans = tds.find("span");
        var inputs = tds.find("input");
        
        if (spans.length == 0) {
        	var val = inputs[0].value.trim();
                       
            if(val == ""){
            	$.alertable.alert("输入值不能为空！");
                return;
            }
            else{
            	inputData[i].value = val;
            }
            
        } else {
        		var uplimit = 0;
        		var lolimit = 0;
        		var configvalue = 0;
        		
                var jsonArr = [];
                for (var j = 0; j < spans.length; j++) {
                    var name = spans[j].title;
                    var text = spans[j].textContent;

                    var val = inputs[j].value.trim();                      
		            if(val == ""){
		            	$.alertable.alert("输入值不能为空！");
		                return;
		            }
		            else{
		            	val = parseFloat(val);
		            }
                    
                    switch(name){
	                    case "uplimit":
	                    	uplimit = val;
	                    	break;
	                    case "lolimit":
	                    	lolimit = val;
	                    	break;
	                    case "configvalue":
	                    	configvalue = val;
	                    	break;
	                    default:
	                    	break;
                    }
                    
                    var arr = {
                        name: name,
                        text: text,
                        value: val
                    };
                    jsonArr.push(arr);
                    
                }
                
                //判断设定值是否在上下限区间内
                if(configvalue >= lolimit && configvalue <= uplimit){
                	inputData[i].value = jsonArr;
                }
                else{
                	$.alertable.alert("输入值错误，提交失败！");
                	return;
                }              
        }     
    }

    //提交
    var res = form_syn.saveXmlConfigFile(JSON.stringify(inputData));

    if (res) {
        $.alertable.info("提交成功！");
        getParameters(); 
    } else {
        $.alertable.alert("提交失败！");
    }

}

window.onkeydown = function (event) {
    if (event.keyCode == 123)
        form_syn.showDebugTools();
}

