function ShowAddingCommentForm(classId) {
    fetch('/Account/IsAuthenticated')
        .then(response => response.json())
        .then(isAuthenticated => {
            if (isAuthenticated) {
                fetch(`/ClassComment/CreateComment/${classId}`)
                    .then(res => res.text())
                    .then(data => {
                        $("#myLargeModalLabel").html("افزودن نظر به کلاس");
                        $("#largeModalBody").html(data);
                        $("#LargeModal").modal('show');
                    })
            } else {
                window.location.href = '/LogIn'; 
            }
        })
    
}

function EditCommentForm(commentId) {
    fetch(`/UserManagement/ClassComment/Edit/${commentId}`)
        .then(res => res.text())
        .then(data => {
            $("#myLargeModalLabel").html("ویرایش نظر");
            $("#largeModalBody").html(data);
            $("#LargeModal").modal('show');
        });
}

function OnSuccessAddingItem(res) {
    if (res.status == 200) {
        Swal.fire({
            title: "موفق",
            text: res.message,
            icon: "success"
        }).then((result) => {
            if (result.isConfirmed) {
                location.reload();
            }
            else {
                setTimeout(() => {
                    location.reload();
                }, 5000);
            }
        })
    }
    else {
        Swal.fire({
            title: "خطا",
            text: res.message,
            icon: "error"
        });
    }
}


document.addEventListener('DOMContentLoaded', function () {
    
    // Add click event listeners to like and dislike buttons
    document.querySelectorAll('.comment-section').forEach(comment => {
        
        var likeButton = comment.querySelector('.like-button');
        var dislikeButton = comment.querySelector('.dislike-button');
        
        // Add click event for the like button
        likeButton.addEventListener('click', function () {
            fetch('/Account/IsAuthenticated')
                .then(response => response.json())
                .then(isAuthenticated => {
                    if (isAuthenticated) {
                        toggleVote(comment, 'Like');
                    } else {
                        window.location.href = '/LogIn'; 
                    }
                })
            
        });

        // Add click event for the dislike button
        dislikeButton.addEventListener('click', function () {
            fetch('/Account/IsAuthenticated')
                .then(response => response.json())
                .then(isAuthenticated => {
                    if (isAuthenticated) {
                        toggleVote(comment, 'DisLike');
                    } else {
                        window.location.href = '/LogIn';
                    }
                })           
        });
    });
});

function toggleVote(commentElement, voteType) {
    const commentId = parseInt(commentElement.dataset.commentId);
    const likeButton = commentElement.querySelector('.like-button');
    const dislikeButton = commentElement.querySelector('.dislike-button');
    // Reset classes
    likeButton.classList.remove('bxs-like');
    dislikeButton.classList.remove('bxs-dislike');

    // Apply the selected class to the clicked button
    if (voteType === 'Like') {
        likeButton.classList.add('bxs-like');
        dislikeButton.classList.add('bx-dislike');
    } else {
        dislikeButton.classList.add('bxs-dislike');
        likeButton.classList.add('bx-like');
    }

    const commentReaction = voteType;
    const classId = parseInt(commentElement.dataset.classId);
    var model = {
        commentId: commentId,
        commentReaction: 'commentReaction',
        classId: classId
    }
    $.ajax({
        url: '/ClassComment/CommentVoteCreate',
        contentType:'application/json; charset=utf-8',
        //data: strjson,
        data: { commentId, commentReaction, classId },
        type: 'GET',
        success: function (response) {
            const linkecount = commentElement.querySelector('.like-count');
            const dislinkecount = commentElement.querySelector('.dislike-count');           
            if (response.likeAmount == 0) {
                response.likeAmount = "";              
            }
            if (response.dislikeAmount == 0) {
                response.dislikeAmount = "";                
            }
            linkecount.textContent = response.likeAmount;
            dislinkecount.textContent = response.dislikeAmount;
            
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
        }
    })
}