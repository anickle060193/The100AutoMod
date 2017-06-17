( function()
{
    var chatMessageInput = document.querySelector( '#chatroom .inputMessage' );
    if( !chatMessageInput )
    {
        throw 'Could not find chat message input.';
    }

    var chatMessageSubmit = document.querySelector( '#chatSubmit' );
    if( !chatMessageSubmit )
    {
        throw 'Could not find chat message submit button.';
    }

    chatMessageInput.value = `{chatMessage}`;
    chatMessageSubmit.click();
} )();