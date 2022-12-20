namespace echart_app {

    /**
     * ECharts.js 必须要在当前模块初始化之前被引用
    */
    export const echarts: echarts_factory = (<any>window).echarts;

    export interface echarts_canvas {
        setOption(option: {}): void;
        resize(): void;
    }

    export interface echarts_factory {
        init(dom: HTMLElement, any, opt: {
            renderer: string,
            useDirtyRect: boolean
        });
    }

    export interface options {
        
    }
}