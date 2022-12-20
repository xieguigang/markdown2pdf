namespace echart_app {

    /**
     * ECharts.js 必须要在当前模块初始化之前被引用
    */
    export const echarts: echarts_factory = (<any>window).echarts;

    /**
     * The paper color set
    */
    export const paper: string[] = [
        "#d02823", "#0491d0", "#88bb64", "#15dbff", "#583b73",
        "#f2ce3f", "#8858bf", "#ccff33", "#00ff00", "#0000a0",
        "#41b6ab", "#f0bf59", "#79c753", "#c02034", "#097988",
        "#ff1bff"
    ];

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

    export interface options<T extends serial_data> {
        series: T[];
    }

    export interface serial_data {
        type: string;
        name: string;
        data: any[];
        symbol?: string;
        symbolSize?: number;
        itemStyle: {
            borderWidth?: number,
            color: string,
            borderColor?: string,
            normal?: {
                shadowBlur: number,
                shadowColor: string
            }
        }
    }
}