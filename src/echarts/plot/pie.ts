namespace plot {

    export interface pieAdapter<T> {
        (data: T): echart_app.options;
    }

    export class pie<T> extends canvas {

        public constructor(private adapter: pieAdapter<T>, id = "container") {
            super(id);
        }

        protected loadOptions(data: any): echart_app.options {
            const opt = this.adapter(data);
            return opt;
        }
    }
}