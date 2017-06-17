( function()
{
    var homePageHtmlInput = document.getElementById( 'html' );
    if( !homePageHtmlInput )
    {
        throw 'Could not find home page html input.';
    }

    function waitForHomePageHtml()
    {
        if( homePageHtmlInput.value )
        {
            The100BoundMod.onHomePageHtmlReceived( homePageHtmlInput.value );
        }
        else
        {
            setTimeout( waitForHomePageHtml, 100 );
        }
    }
    
    waitForHomePageHtml();

} )();