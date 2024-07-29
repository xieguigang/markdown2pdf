namespace plot {

    export interface bardata extends echart_app.serial_data {

    }

    export interface bar_option extends echart_app.options<bardata> {
        xAxis: {
            type:"value",
            boundaryGap: [0, 0.01]
        };
        yAxis: {
            type: 'category',
            data: string[]
        };

    }

    export class barplot<T> extends canvas {

    }
}