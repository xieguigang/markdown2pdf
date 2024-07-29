namespace echart_app {

    /**
     * a common data model for the data serial
    */
    export interface serial_data {
        type: string;
        name: string;
        /**
         * usually be a numeric vector
        */
        data: any[];
        symbol?: string;
        symbolSize?: number;
        itemStyle: {
            borderWidth?: number,
            color: string,
            borderColor?: string,
            normal?: {
                shadowBlur: number,
                shadowColor: string
            }
        }
    }
}