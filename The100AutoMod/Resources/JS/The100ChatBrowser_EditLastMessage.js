( function()
{
    var lastMessage = document.querySelector( '[data-from="D633_Automod"]' );
    if( !lastMessage )
    {
        throw 'Could not find last message.';
    }

    var editLink = lastMessage.querySelector( 'a.edit-message' );
    if( !editLink )
    {
        throw 'Could not find edit link for last message.';
    }

    editLink.click();

    var editMessageInput = document.querySelector( 'form.edit-message input.inputMessage' );
    if( !editMessageInput )
    {
        throw 'Could not find edit message input.';
    }

    var editMessageSubmit = document.querySelector( 'form.edit-message button.edit-message' );
    if( !editMessageSubmit )
    {
        throw 'Could not find edit message submit button.';
    }

    editMessageInput.value = `{editChatMessage}`;
    editMessageSubmit.click();
} )();