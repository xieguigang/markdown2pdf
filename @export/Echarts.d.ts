// export R# package module type define for javascript/typescript language
//
//    imports "Echarts" from "reportKit";
//
// ref=reportKit.Echarts@reportKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

/**
 * Echarts plot data helper, generate echart plot data object from .net clr object
 * 
*/
declare namespace Echarts {
   /**
    * Convert the sciBASIC.NET network graph object as the e-charts graph object
    * 
    * 
     * @param g -
   */
   function json_graph(g: object): object;
}
