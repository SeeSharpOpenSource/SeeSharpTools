$(function () {
    var sin = [], cos = [];
    for (var i = 0; i < 5 * Math.PI; i += 0.2) {
        sin.push([i, Math.sin(i) * Math.sin(i)]);
        cos.push([i, Math.cos(i)]);
    }

    var plot = $.plot($("#mws-line-chart"),
           [ { data: sin, label: "Sin(x) * Sin(x)", color: "#c75d7b"}, { data: cos, label: "Cos(x)", color: "#c5d52b" } ], {
               series: {
                   lines: { show: true },
                   points: { show: true }
               },
               grid: { hoverable: true, clickable: true }
             });
    
    var data = [
                {label: "American", data: 41}, 
                {label: "Indonesian", data: 12}, 
                {label: "Dutch", data: 55}, 
                {label: "Japanese", data: 12}, 
                {label: "Korean", data: 11}, 
                {label: "French", data: 66}, 
                {label: "Chinese", data: 11}
                ];             
    
    $.plot($("#mws-pie-1"), 
    	    data, {
    		        series: {
    		            pie: { 
    		                show: true
    		            }
    		        }
    		});
    
    $.plot($("#mws-pie-2"), 
    	    data, {
    		        series: {
    		            pie: { 
    		                show: true
    		            }
    		        }, 
    		        legend: {
    		        	show:false
    		        }
    		});
    
    var d1 = [];
    for (var i = 0; i <= 10; i += 1)
        d1.push([i, parseInt(Math.random() * 30)]);

    var d2 = [];
    for (var i = 0; i <= 10; i += 1)
        d2.push([i, parseInt(Math.random() * 30)]);

    var d3 = [];
    for (var i = 0; i <= 10; i += 1)
        d3.push([i, parseInt(Math.random() * 30)]);

    var stack = 0, bars = true, lines = false, steps = false;
    
    $.plot($("#mws-bar-chart"), [ d1, d2, d3 ], {
        series: {
            stack: stack,
            lines: { show: lines, fill: true, steps: steps },
            bars: { show: bars, barWidth: 0.6 }
        }
    });
});