jq(function() {
    jq('#submit-form').click(function(){
        //Verify Form
        jq('form').submit();
    });

    jq('#DoB').on('blur', function(){
        jq.ajax({
            dataType: "text",
            url: '/Registration/GetBirthdateFromString',
            data: {
                "value": jq(this).val()
            },
            success: function(results) {
                jq('#DoB').val(results);
            },
            error: function(xhr, ajaxOptions, thrownError) {
                console.log(xhr.status);
                console.log(thrownError);
            }
        });
    });
});