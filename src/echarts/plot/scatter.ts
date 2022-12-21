/// <reference path="plotApp.ts" />

namespace plot {

    export interface scatterAdapter<T> {
        (data: T): scatter_option;
    }

    export interface scatter_option extends echart_app.options<scatter_data> {
        xAxis: {};
        yAxis: {};

    }

    export interface scatter_data extends echart_app.serial_data {
        symbolSize: number;

    }

    export class scatter<T> extends canvas {

        public constructor(private adapter: scatterAdapter<T>, id = "container") {
            super(id);
        }

        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T> {
            return <any>this.adapter(data);
        }
    }
}