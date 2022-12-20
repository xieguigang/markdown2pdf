/// <reference path="plotApp.ts" />

namespace plot {

    export interface scatterAdapter<T> {
        (data: T): echart_app.options;
    }

    export class scatter<T> extends canvas {

        public constructor(private adapter: scatterAdapter<T>, id = "container") {
            super(id);
        }

        protected loadOptions(data: T): echart_app.options {
            return this.adapter(data);
        }
    }
}