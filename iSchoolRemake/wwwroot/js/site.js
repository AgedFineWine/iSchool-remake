// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// when the body is ready

// use a map to store the course data that has already been fetched
const minors = new Map();

$(function () {
    // run the accordion for degrees
    $(".grad-acc, .undergrad-acc, .minor-acc").accordion({
        collapsible: true,
        active: false,
        heightStyle: "content"
    });

    // run the data tables
    $(".coop-table, .employ-table").DataTable();

    // listen to clicks for minor modal
    $(".course-btn").on('click', function () {
        const courseTitle = $(this).data('title');
        if (minors.has(courseTitle)) {
            //console.log(minors.get(courseTitle))
            $('.modal-body').html(`${minors.get(courseTitle).description}`);
            $('.modal-title').html(`${minors.get(courseTitle).title}`);
            return;
        }

        $.ajax({
            url: `https://people.rit.edu/~dsbics/proxy/https://ischool.gccis.rit.edu/api/course/courseID=${courseTitle}`,
            method: 'GET',
            success: function (data) {
                // Update modal with API response
                $('.modal-body').html(`
                      ${data.description}
                    `);
                $('.modal-title').html(`
                    ${data.title}
                `);
                minors.set(courseTitle, data);
            },
            error: function () {
                $('.modal-body').html('<p>Error loading course details.</p>');
            }
        });
    });

    // activate the tabs for the people page
    $("#pepTab").tabs();
    $("#allPeople").fadeIn(500);

    $("#footer-container").load("/Home/Footer");
})