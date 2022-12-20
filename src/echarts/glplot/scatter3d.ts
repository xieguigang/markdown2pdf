/// <reference path="../plot/plotApp.ts" />

namespace gl_plot {

    export interface scatterAdapter<T> {
        (data: T): scatter3d_options;
    }

    export interface scatter3d_options extends echart_app.options<gl_scatter_data> {
        grid3D: {},
        xAxis3D: gl_axis,
        yAxis3D: gl_axis,
        zAxis3D: gl_axis
    }

    export interface gl_scatter_data extends echart_app.serial_data {
        dimensions: string[];
    }

    export interface gl_axis {
        type: string,
        name: string
    }

    export class scatter3d<T> extends plot.canvas {

        public constructor(private adapter: scatterAdapter<T>, id = "container") {
            super(id);
        }

        protected loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T> {
            const opt: scatter3d_options = this.adapter(data);

            opt.grid3D = opt.grid3D || {};
            opt.xAxis3D = opt.xAxis3D || <any>{ type: "value" };
            opt.yAxis3D = opt.yAxis3D || <any>{ type: "value" };
            opt.zAxis3D = opt.zAxis3D || <any>{ type: "value" };

            return <any>opt;
        }
    }
}