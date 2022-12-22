var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
        var canvas = document.getElementById(id);
        if (canvas) {
            canvas.removeAttribute("_echarts_instance_");
            canvas.innerHTML = "";
        }
    }
    echart_app.clear = clear;
})(echart_app || (echart_app = {}));
var plot;
(function (plot) {
    var canvas = /** @class */ (function () {
        /**
         * @param id the html element node id, which should be
         *    no ``#`` symbol prefix, example value as "container".
        */
        function canvas(id) {
            echart_app.clear(id);
            canvas.check_env();
            this.dom = document.getElementById(id);
            this.echart = echart_app.echarts.init(this.dom, null, {
                renderer: 'canvas',
                useDirtyRect: false
            });
        }
        Object.defineProperty(canvas.prototype, "chartObj", {
            get: function () {
                return this.echart;
            },
            enumerable: true,
            configurable: true
        });
        canvas.check_env = function () {
            if (!echart_app.echarts) {
                throw "The ECharts should be imports before this script module is loaded!";
            }
        };
        canvas.prototype.plot = function (data) {
            var option = this.loadOptions(data);
            var vm = this.echart;
            vm.setOption(option);
            window.addEventListener('resize', vm.resize);
        };
        return canvas;
    }());
    plot.canvas = canvas;
})(plot || (plot = {}));
/// <reference path="../plot/plotApp.ts" />
var gl_plot;
(function (gl_plot) {
    var scatter3d = /** @class */ (function (_super) {
        __extends(scatter3d, _super);
        function scatter3d(adapter, id) {
            if (id === void 0) { id = "container"; }
            var _this = _super.call(this, id) || this;
            _this.adapter = adapter;
            return _this;
        }
        scatter3d.prototype.loadOptions = function (data) {
            var opt = this.adapter(data);
            opt.grid3D = opt.grid3D || {};
            opt.xAxis3D = opt.xAxis3D || { type: "value" };
            opt.yAxis3D = opt.yAxis3D || { type: "value" };
            opt.zAxis3D = opt.zAxis3D || { type: "value" };
            return opt;
        };
        return scatter3d;
    }(plot.canvas));
    gl_plot.scatter3d = scatter3d;
})(gl_plot || (gl_plot = {}));
var plot;
(function (plot) {
    var heatmap = /** @class */ (function (_super) {
        __extends(heatmap, _super);
        function heatmap(adapter, colorSet, id) {
            if (colorSet === void 0) { colorSet = echart_app.jet; }
            if (id === void 0) { id = "container"; }
            var _this = _super.call(this, id) || this;
            _this.adapter = adapter;
            _this.colorSet = colorSet;
            return _this;
        }
        heatmap.prototype.loadOptions = function (data) {
            var opt = this.adapter(data);
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
        };
        return heatmap;
    }(plot.canvas));
    plot.heatmap = heatmap;
})(plot || (plot = {}));
var plot;
(function (plot) {
    var histogramPlot = /** @class */ (function (_super) {
        __extends(histogramPlot, _super);
        function histogramPlot(adapter, id) {
            if (id === void 0) { id = "container"; }
            var _this = _super.call(this, id) || this;
            _this.adapter = adapter;
            return _this;
        }
        histogramPlot.prototype.loadOptions = function (data) {
            var opt = this.adapter(data);
            return opt;
        };
        return histogramPlot;
    }(plot.canvas));
    plot.histogramPlot = histogramPlot;
})(plot || (plot = {}));
/// <reference path="plotApp.ts" />
var plot;
(function (plot) {
    var pie = /** @class */ (function (_super) {
        __extends(pie, _super);
        function pie(adapter, id) {
            if (id === void 0) { id = "container"; }
            var _this = _super.call(this, id) || this;
            _this.adapter = adapter;
            return _this;
        }
        pie.prototype.loadOptions = function (data) {
            var opt = this.adapter(data);
            return opt;
        };
        return pie;
    }(plot.canvas));
    plot.pie = pie;
})(plot || (plot = {}));
/// <reference path="plotApp.ts" />
var plot;
(function (plot) {
    var scatter = /** @class */ (function (_super) {
        __extends(scatter, _super);
        function scatter(adapter, id) {
            if (id === void 0) { id = "container"; }
            var _this = _super.call(this, id) || this;
            _this.adapter = adapter;
            return _this;
        }
        scatter.prototype.loadOptions = function (data) {
            return this.adapter(data);
        };
        return scatter;
    }(plot.canvas));
    plot.scatter = scatter;
})(plot || (plot = {}));
//# sourceMappingURL=echarts@types.js.map