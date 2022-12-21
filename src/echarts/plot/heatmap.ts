namespace plot {

    export interface heatmap_data extends echart_app.serial_data {
        progressive: number;
        animation: boolean;
        emphasis: {
            itemStyle: {
                borderColor: string,
                borderWidth: number
            }
        }
    }

    export interface heatmap_option extends echart_app.options<heatmap_data> {
        visualMap: {
            min: number,
            max: number,
            calculable: boolean,
            realtime: boolean,
            inRange: {
                color: string[]
            }
        }
    }

    export interface heatmapAdapter<T> {
        (data: T): echart_app.options<heatmap_data>;
    }

    export class heatmap<T> extends plot.canvas {

        public constructor(
            private adapter: heatmapAdapter<T>,
            private colorSet: string[] = echart_app.paper,
            id = "container"
        ) {
            super(id);
        }

        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T> {
            const opt: heatmap_option = <any>this.adapter(data);

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

            return <any>opt;
        }

    }
}