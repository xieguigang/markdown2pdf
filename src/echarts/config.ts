namespace echart_app {

    /**
     * a common model of the ehcarts plot configuration
    */
    export interface options<T extends serial_data> {
        series: T[];
        color?: string[];
        tooltip?: {};
        title?: {
            text: string;
            subtext: string;
            sublink: string;
            left: string | number;
            top: string | number;
        }
    }
}