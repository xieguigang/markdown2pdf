declare namespace echart_app {
    /**
     * ECharts.js 必须要在当前模块初始化之前被引用
    */
    const echarts: echarts_factory;
    /**
     * The paper color set
    */
    const paper: string[];
    interface echarts_canvas {
        setOption(option: {}): void;
        resize(): void;
        on(evt: string, handle: (any: any) => void): any;
    }
    interface echarts_factory {
        init(dom: HTMLElement, any: any, opt: {
            renderer: string;
            useDirtyRect: boolean;
        }): any;
    }
    interface options<T extends serial_data> {
        series: T[];
        color?: string[];
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
    }
    interface scatter_data extends echart_app.serial_data {
    }
    class scatter<T> extends canvas {
        private adapter;
        constructor(adapter: scatterAdapter<T>, id?: string);
        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T>;
    }
}
