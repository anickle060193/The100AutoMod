( function()
{
    var target = document.querySelector( "ul.messages" );

    if( target )
    {
        var config = { childList: true };

        var loadedTime = new Date().getTime();

        var observer = new MutationObserver( function( mutations )
        {
            mutations.forEach( function( mutation )
            {
                mutation.addedNodes.forEach( function( addedNode )
                {
                    var username = addedNode.dataset.from;
                    var timeAgo = addedNode.querySelector( '[data-time-ago]' ).dataset.timeAgo;
                    var content = addedNode.querySelector( '.messageBody' ).innerText;

                    if( timeAgo > loadedTime )
                    {
                        The100AutoMod.onChatMessageReceived( username, timeAgo, content );
                    }
                } );
            } );
        } );

        observer.observe( target, config );
    }
} )();