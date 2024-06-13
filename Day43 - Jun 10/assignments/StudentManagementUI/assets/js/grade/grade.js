AOS.init({ duration: 1000 });

$(document).ready(function () {
  // Define custom sorting for grades
  const gradeOrder = {
    O: 1,
    "A+": 2,
    A: 3,
    "B+": 4,
    B: 5,
    C: 6,
    F: 7,
    UA: 8,
    RA: 9,
  };

  jQuery.fn.dataTable.ext.type.order["grade-order-pre"] = function (d) {
    return gradeOrder[d] || 10; // default to a high number for unrecognized grades
  };

  // Initialize the DataTable
  const table = $("#gradeTable").DataTable({
    // Disable sorting on the last column
    columnDefs: [
      { orderable: false, targets: 5 },
      { type: "grade-order", targets: 3 }, // Apply custom sorting to the grade column
    ],
    columns: [null, null, null, null, null, { searchable: false }],
    pagingType: "full_numbers",
    // Set default page length
    pageLength: 10,
    language: {
      // Customize pagination prev and next buttons: use arrows instead of words
      paginate: {
        previous: '<span class="fa fa-chevron-left"></span>',
        next: '<span class="fa fa-chevron-right"></span>',
        first: '<span class="fa-solid fa-angles-left"></span>',
        last: '<span class="fa-solid fa-angles-right"></span>',
      },
      // Customize number of elements to be displayed
      lengthMenu:
        'Display <select class="form-control input-sm">' +
        '<option value="3">3</option>' +
        '<option value="5">5</option>' +
        '<option value="10">10</option>' +
        '<option value="15">15</option>' +
        '<option value="20">20</option>' +
        '<option value="25">25</option>' +
        '<option value="-1">All</option>' +
        "</select> results",
    },
  });
});
