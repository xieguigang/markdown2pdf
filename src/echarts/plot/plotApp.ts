namespace plot {

    export abstract class canvas {

        protected dom: HTMLElement
        protected echart: echart_app.echarts_canvas;

        public constructor(id: string = "container") {
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

        protected abstract loadOptions<T>(data: T): {};

        public plot<T>(data: T) {
            const option = this.loadOptions(data);
            const vm = this.echart;

            vm.setOption(option);
            window.addEventListener('resize', vm.resize);
        }
    }
}