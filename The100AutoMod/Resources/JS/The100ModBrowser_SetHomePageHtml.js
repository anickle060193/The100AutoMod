( function()
{
    var homePageHtmlInput = document.getElementById( 'group_latest_news_html' );
    if( !homePageHtmlInput )
    {
        throw 'Failed to find "Latest New HTML" input.';
    }

    homePageHtmlInput.value = `{homePageHtml}`;

    var submitButton = document.querySelector( '#edit_group_268 input[type="submit"]' );
    if( !submitButton )
    {
        throw 'Failed to find Submit button.';
    }

    submitButton.click();
} )();