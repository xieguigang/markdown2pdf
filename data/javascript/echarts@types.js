var echart_app;
(function (echart_app) {
    /**
     * ECharts.js 必须要在当前模块初始化之前被引用
    */
    echart_app.echarts = window.echarts;
    /**
     * The paper color set
    */
    echart_app.paper = [
        "#d02823", "#0491d0", "#88bb64", "#15dbff", "#583b73",
        "#f2ce3f", "#8858bf", "#ccff33", "#00ff00", "#0000a0",
        "#41b6ab", "#f0bf59", "#79c753", "#c02034", "#097988",
        "#ff1bff"
    ];
    echart_app.jet = [
        "#00007F",
        "#0000FF",
        "#007FFF",
        "#00FFFF",
        "#7FFF7F",
        "#FFFF00",
        "#FF7F00",
        "#FF0000",
        "#7F0000" // dark red
    ];
    function clear(id) {
        const canvas = document.getElementById(id);
        if (canvas) {
            canvas.removeAttribute("_echarts_instance_");
            canvas.innerHTML = "";
        }
    }
    echart_app.clear = clear;
})(echart_app || (echart_app = {}));
var plot;
(function (plot) {
    class canvas {
        /**
         * @param id the html element node id, which should be
         *    no ``#`` symbol prefix, example value as "container".
        */
        constructor(id) {
            echart_app.clear(id);
            canvas.check_env();
            this.dom = document.getElementById(id);
            this.echart = echart_app.echarts.init(this.dom, null, {
                renderer: 'canvas',
                useDirtyRect: false
            });
        }
        get chartObj() {
            return this.echart;
        }
        static check_env() {
            if (!echart_app.echarts) {
                throw "The ECharts should be imports before this script module is loaded!";
            }
        }
        plot(data) {
            const option = this.loadOptions(data);
            const vm = this.echart;
            vm.setOption(option);
            window.addEventListener('resize', vm.resize);
        }
    }
    plot.canvas = canvas;
})(plot || (plot = {}));
/// <reference path="../plot/plotApp.ts" />
var gl_plot;
(function (gl_plot) {
    class scatter3d extends plot.canvas {
        constructor(adapter, id = "container") {
            super(id);
            this.adapter = adapter;
        }
        loadOptions(data) {
            const opt = this.adapter(data);
            opt.grid3D = opt.grid3D || {};
            opt.xAxis3D = opt.xAxis3D || { type: "value" };
            opt.yAxis3D = opt.yAxis3D || { type: "value" };
            opt.zAxis3D = opt.zAxis3D || { type: "value" };
            return opt;
        }
    }
    gl_plot.scatter3d = scatter3d;
})(gl_plot || (gl_plot = {}));
var plot;
(function (plot) {
    class heatmap extends plot.canvas {
        constructor(adapter, colorSet = echart_app.jet, id = "container") {
            super(id);
            this.adapter = adapter;
            this.colorSet = colorSet;
        }
        loadOptions(data) {
            const opt = this.adapter(data);
            if (!opt.visualMap) {
                opt.visualMap = {
                    min: 0,
                    max: 1,
                    calculable: true,
                    realtime: false,
                    inRange: {
                        color: this.colorSet
                    }
                };
            }
            return opt;
        }
    }
    plot.heatmap = heatmap;
})(plot || (plot = {}));
/// <reference path="plotApp.ts" />
var plot;
(function (plot) {
    class pie extends plot.canvas {
        constructor(adapter, id = "container") {
            super(id);
            this.adapter = adapter;
        }
        loadOptions(data) {
            const opt = this.adapter(data);
            return opt;
        }
    }
    plot.pie = pie;
})(plot || (plot = {}));
/// <reference path="plotApp.ts" />
var plot;
(function (plot) {
    class scatter extends plot.canvas {
        constructor(adapter, id = "container") {
            super(id);
            this.adapter = adapter;
        }
        loadOptions(data) {
            return this.adapter(data);
        }
    }
    plot.scatter = scatter;
})(plot || (plot = {}));
//# sourceMappingURL=echarts@types.js.map