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
    }
    interface echarts_factory {
        init(dom: HTMLElement, any: any, opt: {
            renderer: string;
            useDirtyRect: boolean;
        }): any;
    }
    interface options {
    }
}
declare namespace plot {
    abstract class canvas {
        protected dom: HTMLElement;
        protected echart: echart_app.echarts_canvas;
        /**
         * @param id the html element node id, which should be
         *    no ``#`` symbol prefix, example value as "container".
        */
        constructor(id: string);
        private static check_env;
        protected abstract loadOptions(data: any): echart_app.options;
        plot(data: any): void;
    }
}
declare namespace gl_plot {
    interface scatterAdapter<T> {
        (data: T): echart_app.options;
    }
    class scatter3d<T> extends plot.canvas {
        private adapter;
        constructor(adapter: scatterAdapter<T>, id?: string);
        protected loadOptions(data: any): echart_app.options;
    }
}
declare namespace plot {
    interface pieAdapter<T> {
        (data: T): echart_app.options;
    }
    class pie<T> extends canvas {
        private adapter;
        constructor(adapter: pieAdapter<T>, id?: string);
        protected loadOptions(data: any): echart_app.options;
    }
}
declare namespace plot {
    interface scatterAdapter<T> {
        (data: T): echart_app.options;
    }
    class scatter<T> extends canvas {
        private adapter;
        constructor(adapter: scatterAdapter<T>, id?: string);
        protected loadOptions(data: T): echart_app.options;
    }
}
