namespace gl_plot {

    export interface scatterAdapter<T> {
        (data: T): echart_app.options;
    }

    interface scatter3d_options extends echart_app.options {

    }

    export class scatter3d<T> extends plot.canvas {

        public constructor(private adapter: scatterAdapter<T>, id = "container") {
            super(id);
        }

        protected loadOptions(data: any): echart_app.options {
            const opt: scatter3d_options = this.adapter(data);

            return opt;
        }
    }
}