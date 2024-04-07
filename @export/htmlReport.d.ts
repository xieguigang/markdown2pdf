// export R# package module type define for javascript/typescript language
//
//    imports "htmlReport" from "reportKit";
//
// ref=reportKit.htmlReportEngine@reportKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

/**
 * html templat handler
 * 
*/
declare namespace htmlReport {
   /**
     * @param orders default value Is ``null``.
     * @param figStart default value Is ``1``.
     * @param prefix default value Is ``'fig'``.
     * @param format default value Is ``'p #. '``.
     * @param env default value Is ``null``.
   */
   function countFigures(report: any, orders?: string, figStart?: object, prefix?: string, format?: string, env?: object): any;
   /**
     * @param orders default value Is ``null``.
     * @param tableStart default value Is ``1``.
     * @param prefix default value Is ``'table'``.
     * @param format default value Is ``'p #. '``.
     * @param env default value Is ``null``.
   */
   function countTables(report: any, orders?: string, tableStart?: object, prefix?: string, format?: string, env?: object): any;
   /**
   */
   function encodeLocalURL(filepath: string): string;
   /**
   */
   function exportJSON(report: object): object;
   /**
    * save the modified interpolated html
    *  template data onto the disk file.
    * 
    * 
     * @param template -
     * @param outputdir export of the page files into this new output 
     *  directory instead of export to the source 
     *  folder where this report object is loaded via 
     *  the ``reportTemplate`` function.
     * 
     * + default value Is ``null``.
   */
   function flush(template: object, outputdir?: string): boolean;
   /**
    * 
    * 
     * @param image_class 
     * + default value Is ``null``.
     * @param image_url apply for the document template rendering
     * 
     * + default value Is ``null``.
     * @param framework 
     * + default value Is ``'bootstrap'``.
     * @param env 
     * + default value Is ``null``.
   */
   function html_render(image_class?: any, image_url?: any, framework?: string, env?: object): object|object;
   /**
    * Create a html template model from the given template file
    * 
    * 
     * @param url -
   */
   function htmlTemplate(url: string): object;
   /**
    * do report data interpolation.
    * 
    * 
     * @param template -
     * @param metadata -
     * @param env -
     * 
     * + default value Is ``null``.
   */
   function interpolate(template: object, metadata: object, env?: object): object;
   /**
    * Load resource files for build html report
    *  
    *  load the resource files based on the description list data, 
    *  and then generates the html segments for the resource files.
    * 
    * 
     * @param description the resource file contents in this description data 
     *  supports file types:
     *  
     *  1. ``*.txt`` for plant text file
     *  2. ``*.csv`` for data table file
     *  3. ``*.png/jpg/bmp`` for raster image file
     *  4. ``*.svg`` for vector image file
     *  5. ``*.html`` for html text file
     *  6. ``*.md`` for makrdown text file, the markdown text file 
     *       will be rendering as the html document text at first
     *       and then returns a html document to the rendering 
     *       engine.
     * @param workdir -
     * 
     * + default value Is ``null``.
     * @param meta -
     * 
     * + default value Is ``null``.
     * @param env -
     * 
     * + default value Is ``null``.
   */
   function loadResource(description: object, workdir?: any, meta?: object, env?: object): object;
   module markdown {
      /**
       * Render markdown to html text
       * 
       * 
        * @param markdown -
        * @param htmlRender 
        * + default value Is ``null``.
      */
      function html(markdown: string, htmlRender?: object): string;
      /**
      */
      function latex(markdown: string): string;
   }
   /**
   */
   function pageBreak(): string;
   /**
     * @param orders default value Is ``null``.
     * @param headerStart default value Is ``'1'``.
     * @param env default value Is ``null``.
   */
   function pageHeaders(report: any, orders?: string, headerStart?: string, env?: object): any;
   /**
    * assign the page numbers to the html templates
    * 
    * 
     * @param report -
     * @param orders the file basename of the html files
     * @param pageStart 
     * + default value Is ``1``.
     * @param env 
     * + default value Is ``null``.
   */
   function pageNumbers(report: object, orders: string, pageStart?: object, env?: object): object;
   /**
    * Create a html template model from a 
    *  given report template directory.
    * 
    * 
     * @param template the zip file package or the template html resource directory path
     * @param copyToTemp 
     * + default value Is ``true``.
   */
   function reportTemplate(template: string, copyToTemp?: boolean): object;
}
