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

    /**
     * The jet color set for heatmap plot
    */
    export const jet: string[] = [
        "#00007F",// dark blue
        "#0000FF",// blue
        "#007FFF",// azure
        "#00FFFF",// cyan
        "#7FFF7F",// light green
        "#FFFF00",// yellow
        "#FF7F00",// orange
        "#FF0000",// red
        "#7F0000" // dark red
    ];

    /**
     * clear target node and then returns target node
    */
    export function clear(id: string) {
        const canvas: HTMLElement = document.getElementById(id);

        if (canvas) {
            canvas.removeAttribute("_echarts_instance_");
            canvas.innerHTML = "";
        }

        return canvas;
    }

    export interface echarts_canvas {
        setOption(option: {}): void;
        resize(): void;
        on(evt: string, handle: (a: evt_argument) => void);
        dispatchAction(arg: { type: string, dataIndex: number });
    }

    export interface evt_argument {
        dataIndex: number;
        name: string;
    }

    export interface echarts_factory {
        init(dom: HTMLElement, any?, opt?: {
            renderer: string,
            useDirtyRect: boolean
        });
    }
}