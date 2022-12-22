declare namespace echart_app {
    /**
     * ECharts.js 必须要在当前模块初始化之前被引用
    */
    const echarts: echarts_factory;
    /**
     * The paper color set
    */
    const paper: string[];
    const jet: string[];
    function clear(id: string): void;
    interface echarts_canvas {
        setOption(option: {}): void;
        resize(): void;
        on(evt: string, handle: (a: evt_argument) => void): any;
        dispatchAction(arg: {
            type: string;
            dataIndex: number;
        }): any;
    }
    interface evt_argument {
        dataIndex: number;
        name: string;
    }
    interface echarts_factory {
        init(dom: HTMLElement, any?: any, opt?: {
            renderer: string;
            useDirtyRect: boolean;
        }): any;
    }
    interface options<T extends serial_data> {
        series: T[];
        color?: string[];
        tooltip?: {};
        title?: {
            text: string;
            subtext: string;
            sublink: string;
            left: string | number;
            top: string | number;
        };
    }
    interface serial_data {
        type: string;
        name: string;
        data: any[];
        symbol?: string;
        symbolSize?: number;
        itemStyle: {
            borderWidth?: number;
            color: string;
            borderColor?: string;
            normal?: {
                shadowBlur: number;
                shadowColor: string;
            };
        };
    }
}
declare namespace plot {
    abstract class canvas {
        protected dom: HTMLElement;
        protected echart: echart_app.echarts_canvas;
        get chartObj(): echart_app.echarts_canvas;
        /**
         * @param id the html element node id, which should be
         *    no ``#`` symbol prefix, example value as "container".
        */
        constructor(id: string);
        private static check_env;
        protected abstract loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T>;
        plot(data: any): void;
    }
}
declare namespace gl_plot {
    interface scatterAdapter<T> {
        (data: T): scatter3d_options;
    }
    interface scatter3d_options extends echart_app.options<gl_scatter_data> {
        grid3D: {};
        xAxis3D: gl_axis;
        yAxis3D: gl_axis;
        zAxis3D: gl_axis;
    }
    interface gl_scatter_data extends echart_app.serial_data {
        dimensions: string[];
    }
    interface gl_axis {
        type: string;
        name: string;
    }
    class scatter3d<T> extends plot.canvas {
        private adapter;
        constructor(adapter: scatterAdapter<T>, id?: string);
        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T>;
    }
}
declare namespace plot {
    interface heatmap_data extends echart_app.serial_data {
        progressive: number;
        animation: boolean;
        emphasis: {
            itemStyle: {
                borderColor: string;
                borderWidth: number;
            };
        };
    }
    interface heatmap_option extends echart_app.options<heatmap_data> {
        visualMap: {
            min: number;
            max: number;
            calculable: boolean;
            realtime: boolean;
            inRange: {
                color: string[];
            };
        };
    }
    interface heatmapAdapter<T> {
        (data: T): echart_app.options<heatmap_data>;
    }
    class heatmap<T> extends plot.canvas {
        private adapter;
        private colorSet;
        constructor(adapter: heatmapAdapter<T>, colorSet?: string[], id?: string);
        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T>;
    }
}
declare namespace plot {
    interface histogramAdapter<T> {
        (data: T): histogram_options;
    }
    interface histogram_options extends echart_app.options<histogram_data> {
        xAxis: {
            type: string;
            min: number;
            max: number;
            interval: number;
        };
        yAxis: {
            type: string;
        };
    }
    interface histogram_data extends echart_app.serial_data {
        shape: {
            x: number;
            y: number;
            width: number;
            height: number;
        };
        data: number[][];
        encode: {
            x: number[];
            y: number;
            tooltip: number;
            label: number;
        };
    }
    class histogramPlot<T> extends canvas {
        private adapter;
        constructor(adapter: histogramAdapter<T>, id?: string);
        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T>;
    }
}
declare namespace plot {
    interface pieAdapter<T> {
        (data: T): echart_app.options<pie_data>;
    }
    interface pie_data extends echart_app.serial_data {
        radius: string;
    }
    class pie<T> extends canvas {
        private adapter;
        constructor(adapter: pieAdapter<T>, id?: string);
        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T>;
    }
}
declare namespace plot {
    interface scatterAdapter<T> {
        (data: T): scatter_option;
    }
    interface scatter_option extends echart_app.options<scatter_data> {
        xAxis: {};
        yAxis: {};
    }
    interface scatter_data extends echart_app.serial_data {
        symbolSize: number;
    }
    class scatter<T> extends canvas {
        private adapter;
        constructor(adapter: scatterAdapter<T>, id?: string);
        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T>;
    }
}
