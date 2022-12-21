namespace plot {

    export interface histogramAdapter<T> {
        (data: T): histogram_options;
    }

    export interface histogram_options extends echart_app.options<histogram_data> {
        xAxis: {
            type: string;
            min: number;
            max: number;
            interval: number
        },
        yAxis: {
            type: string
        }
    }

    export interface histogram_data extends echart_app.serial_data {
        shape: {
            x: number;
            y: number;
            width: number;
            height: number;
        }
        data: number[][],
        encode: {
            x: number[],
            y: number,
            tooltip: number;
            label: number;
        }
    }

    export class histogramPlot<T> extends canvas {

        public constructor(
            private adapter: histogramAdapter<T>,
            id = "container"
        ) {
            super(id);
        }

        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T> {
            const opt = this.adapter(data);
            return <any>opt;
        }
    }
}