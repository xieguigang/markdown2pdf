namespace plot {

    export abstract class canvas {

        protected dom: HTMLElement
        protected echart: echart_app.echarts_canvas;

        /**
         * @param id the html element node id, which should be
         *    no ``#`` symbol prefix, example value as "container".
        */
        public constructor(id: string) {
            canvas.check_env();

            this.dom = document.getElementById(id);
            this.echart = echart_app.echarts.init(this.dom, null, {
                renderer: 'canvas',
                useDirtyRect: false
            });
        }

        private static check_env() {
            if (!echart_app.echarts) {
                throw "The ECharts should be imports before this script module is loaded!";
            }
        }

        protected abstract loadOptions<T extends echart_app.serial_data>(data: any): echart_app.options<T>;

        public plot(data: any) {
            const option = this.loadOptions(data);
            const vm = this.echart;

            vm.setOption(option);
            window.addEventListener('resize', vm.resize);
        }
    }
}