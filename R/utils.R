#' check the wkhtmltpdf config is valid or not
#' 
check_wkhtmltopdf = function() {
    bin = getOption("wkhtmltopdf");

    if (file.exists(bin)) {
        TRUE;
    } else {       
        # "wkhtmltopdf 0.12.5 (with patched qt)"
        if (startsWith(system("wkhtmltopdf -V"), "wkhtmltopdf")) {
            TRUE;
        } else {
            FALSE;
        }        
    }
}