// export R# package module type define for javascript/typescript language
//
// ref=reportKit.highcharts@reportKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

/**
 * 
*/
declare namespace highcharts.js {
   /**
     * @param divId default value Is ``null``.
     * @param env default value Is ``null``.
   */
   function to_javascript(chart: any, divId?: any, env?: object): string;
   /**
     * @param style default value Is ``'height: 450px;'``.
   */
   function to_html(Javascript: string, divId: string, style?: string): string;
   /**
     * @param title default value Is ``'title'``.
     * @param subtitle default value Is ``'subtitle'``.
     * @param serialName default value Is ``'percentage'``.
     * @param backgroundColor default value Is ``'#ffffff'``.
     * @param env default value Is ``null``.
   */
   function piechart(data: object, title?: string, subtitle?: string, serialName?: string, backgroundColor?: any, env?: object): object;
   /**
    * It is a column char actually
    * 
    * 
     * @param data a dictionary of [name => value] tuple paired list
     * @param title -
     * 
     * + default value Is ``'title'``.
     * @param subtitle -
     * 
     * + default value Is ``'subtitle'``.
     * @param ylab -
     * 
     * + default value Is ``'Y'``.
     * @param serialName -
     * 
     * + default value Is ``'data'``.
     * @param backgroundColor -
     * 
     * + default value Is ``'#ffffff'``.
     * @param display3D 
     * + default value Is ``true``.
     * @param env -
     * 
     * + default value Is ``null``.
   */
   function barchart(data: object, title?: string, subtitle?: string, ylab?: string, serialName?: string, backgroundColor?: any, display3D?: boolean, env?: object): object;
   /**
     * @param title default value Is ``'title'``.
     * @param subtitle default value Is ``'subtitle'``.
     * @param serialName default value Is ``'percentage'``.
     * @param backgroundColor default value Is ``'#ffffff'``.
     * @param env default value Is ``null``.
   */
   function piechart(data:object, title?:string, subtitle?:string, serialName?:string, backgroundColor?:any, env?:object): object;
   /**
     * @param style default value Is ``'height: 450px;'``.
   */
   function to_html(Javascript:string, divId:string, style?:string): string;
   /**
     * @param divId default value Is ``null``.
     * @param env default value Is ``null``.
   */
   function to_javascript(chart:any, divId?:any, env?:object): string;
   function varywide_pieChart(data: object, innerSize?: string, title?: string, subtitle?: string, pointerName?: string, serialName?: string, backgroundColor?: any): object;
   /**
    * 
    * 
     * @param data data mapping of ``[name -> x, y]``
     * @param title -
     * 
     * + default value Is ``'title'``.
     * @param subtitle -
     * 
     * + default value Is ``'subtitle'``.
     * @param xlab -
     * 
     * + default value Is ``'X'``.
     * @param ylab -
     * 
     * + default value Is ``'Y'``.
     * @param serialName -
     * 
     * + default value Is ``'data'``.
     * @param backgroundColor 
     * + default value Is ``'#ffffff'``.
     * @param env -
     * 
     * + default value Is ``null``.
   */
   function varywide_barChart(data:object, title?:string, subtitle?:string, xlab?:string, ylab?:string, serialName?:string, backgroundColor?:any, env?:object): object;
   /**
    * 
    * 
     * @param data -
     * @param innerSize radius size in percentage.
     * 
     * + default value Is ``'20'``.
     * @param title 
     * + default value Is ``'pie chart'``.
     * @param subtitle 
     * + default value Is ``'pie chart'``.
     * @param pointerName 
     * + default value Is ``'item'``.
     * @param serialName 
     * + default value Is ``'serial name'``.
     * @param backgroundColor 
     * + default value Is ``'#ffffff'``.
   */
   function varywide_pieChart(data:object, innerSize?:string, title?:string, subtitle?:string, pointerName?:string, serialName?:string, backgroundColor?:any): object;
   function varywide_barChart(data: object, title?: string, subtitle?: string, xlab?: string, ylab?: string, serialName?: string, backgroundColor?: any, env?: object): object;
}
