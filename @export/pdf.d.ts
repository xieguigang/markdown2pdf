// export R# package module type define for javascript/typescript language
//
// ref=reportKit.pdf@reportKit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

/**
 * 
*/
declare namespace pdf {
   /**
   */
   function logo_html(logo:string): string;
   /**
    * convert the local html documents to pdf document.
    * 
    * > the executable file path of the wkhtmltopdf should be
    * >  configed via ``options(wkhtmltopdf = ...)``.
    * 
     * @param files markdown files or html files
     * @param pdfout -
     * 
     * + default value Is ``'out.pdf'``.
     * @param wwwroot 
     * + default value Is ``'/'``.
     * @param style 
     * + default value Is ``null``.
     * @param resolvedAsDataUri 
     * + default value Is ``false``.
     * @param footer 
     * + default value Is ``null``.
     * @param header 
     * + default value Is ``null``.
     * @param opts 
     * + default value Is ``null``.
     * @param pageOpts 
     * + default value Is ``null``.
     * @param pdf_size 
     * + default value Is ``null``.
     * @param env 
     * + default value Is ``null``.
   */
   function makePDF(files:any, pdfout?:string, wwwroot?:string, style?:string, resolvedAsDataUri?:boolean, footer?:object, header?:object, opts?:object, pageOpts?:object, pdf_size?:object, env?:object): string;
   /**
    * 
    * 
     * @param spacing -
     * 
     * + default value Is ``8``.
     * @param center -
     * 
     * + default value Is ``'-- [page] --'``.
     * @param fontsize -
     * 
     * + default value Is ``14``.
     * @param fontname -
     * 
     * + default value Is ``'Microsoft YaHei'``.
     * @param html Adds a html footer/header
     * 
     * + default value Is ``null``.
   */
   function pdfDecoration(spacing?:number, center?:string, fontsize?:number, fontname?:string, html?:string): object;
   /**
     * @param margintop default value Is ``0``.
     * @param marginleft default value Is ``0``.
     * @param marginright default value Is ``0``.
     * @param marginbottom default value Is ``0``.
     * @param imagequality default value Is ``100``.
     * @param title default value Is ``''``.
   */
   function pdfGlobal_options(margintop?:object, marginleft?:object, marginright?:object, marginbottom?:object, imagequality?:object, title?:string): object;
   /**
     * @param javascriptdelay default value Is ``3000``.
     * @param loaderrorhandling default value Is ``null``.
     * @param debugjavascript default value Is ``true``.
     * @param enableforms default value Is ``true``.
   */
   function pdfPage_options(javascriptdelay?:number, loaderrorhandling?:object, debugjavascript?:boolean, enableforms?:boolean): object;
}
