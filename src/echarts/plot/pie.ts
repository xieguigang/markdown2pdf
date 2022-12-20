/// <reference path="plotApp.ts" />

namespace plot {

    export interface pieAdapter<T> {
        (data: T): echart_app.options<pie_data>;
    }

    export interface pie_data extends echart_app.serial_data {
        radius: string;
    }

    export class pie<T> extends canvas {

        public constructor(private adapter: pieAdapter<T>, id = "container") {
            super(id);
        }

        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T> {
            const opt = this.adapter(data);
            return <any>opt;
        }
    }
}