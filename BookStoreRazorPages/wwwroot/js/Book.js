var indexUrl;

$(document).ready(function () {
    // Remove focus from the AddNewBook button on closing the Modal
    // this fixes a aria-hidden focus error
    $(document).on("click", "#closeCreateModalBtn", function () {
        const addNewBookBtn = document.getElementById("addNewBookBtn");
        setTimeout(() => {
            if (document.activeElement === addNewBookBtn) {
                addNewBookBtn.blur();
            }
            document.body.click();
        }, 20);
    });
    //Same for edit modal
    $(document).on("click", "#closeEditModalBtn", function () {
        const editBookBtn = document.getElementById("editButton");
        setTimeout(() => {
            if (document.activeElement === editBookBtn) {
                editBookBtn.blur();
            }
            document.body.click();
        }, 20);
    });

    // Add New Book
    $(document).on("submit", "#bookForm", function (event) {
        event.preventDefault();

        const formData = new FormData(this);
        const photoContainer = document.querySelector(".book-Photos");
        const files = document.querySelector("#bookFiles").files;
        photoContainer.innerHTML = "";
        const photosHTML = Array.from(files)
            .map((file) => {
                const previewURL = URL.createObjectURL(file);
                return `<img class="mr-3" src="${previewURL}" alt="${file.name}" style="max-width:50px" >`;
            })
            .join("");

        $.ajax({
            url: indexUrl,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            headers: {
                RequestVerificationToken: $("#RequestVerificationToken").val(),
            },
            success: function (response) {
                $("#closeCreateModalBtn").click();

                if (response.success) {
                    alert(response.message || "Book created successfully!");
                    $("#createModal").modal("hide");
                    $(".modal-backdrop").remove();
                    console.log(response);
                    if (response.data) {
                        const book = response.data;
                        const authorsNames = Array.isArray(book.authors)
                            ? book.authors.map((author) => author.name || "").join(", ")
                            : "No authors";

                        const bookRow = `
                                <tr data-id="${book.id}">
                                    <td class="book-name">${book.name}</td>
                                    <td class="book-description">${book.description}</td>
                                    <td class="book-category">${book.category}</td>
                                    <td class="book-price">${book.price}</td>
                                    <td class="book-authors">${authorsNames}</td>
                                    <td class="book-Photos">
                                        ${photosHTML}
                                    </td>
                                    <td>
                                        <button class="btn btn-sm btn-warning btn-edit" data-bs-toggle="modal" data-bs-target="#editModal">Edit</button>
                                        <button class="btn btn-sm btn-danger btn-delete">Delete</button>
                                    </td>
                                </tr>`;
                        $("#bookTable").append(bookRow);
                    }
                    $("#createModal").modal("hide");
                } else {
                    alert(
                        response.message || "An error occurred while creating the book.",
                    );
                }
            },
            error: function (xhr) {
                alert("An error occurred: " + xhr.responseText);
            },
        });
    });

    // Load data into the Edit Modal
    $(document).on("click", ".btn-edit", function () {
        const row = $(this).closest("tr");
        const id = row.data("id");

        $("#editBookId").val(id);
        $("#editBookName").val(row.find(".book-name").text().trim());
        $("#editBookDescription").val(row.find(".book-description").text().trim());
        $("#editBookCategory").val(row.find(".book-category").text().trim());
        $("#editBookPrice").val(row.find(".book-price").text().trim());

        const authorsText = row.find(".book-authors").text().trim();
        const authorNames = authorsText.split(",").map((name) => name.trim());
        $("#editBookAuthors option").prop("selected", false);
        $("#editBookAuthors option").each(function () {
            if (authorNames.includes($(this).text().trim())) {
                $(this).prop("selected", true);
            }
        });

        const photosContainer = $("#currentBookPhotos");
        photosContainer.empty();
        const photos = row.find(".book-Photos img").clone();
        photosContainer.append(photos);

        $("#editBookAuthors").trigger("change");
    });

    // Preview images when user selects files
    document
        .querySelector("#editBookFiles")
        .addEventListener("change", function () {

            const files = this.files;

            const photoPreview = document.querySelector("#currentBookPhotos");
            console.log("before", photoPreview);
            photoPreview.innerHTML = "";
            // Generate and append new previews
            const photosHTML = Array.from(files)
                .map((file) => {
                    const previewURL = URL.createObjectURL(file);
                    return `<img class="mr-3" src="${previewURL}" alt="${file.name}" style="max-width:50px">`;
                })
                .join("");

            photoPreview.innerHTML = photosHTML;
            console.log("After", photoPreview);
        });

    // Submit the Edit Modal Form
    $(document).on("submit", "#editBookForm", function (event) {
        event.preventDefault();

        const bookId = $("#editBookId").val();
        const formData = new FormData(this);

        $.ajax({
            url: "?handler=Edit&id=" + bookId,
            type: "POST",
            data: formData,
            processData: false,
            contentType: false,
            headers: {
                RequestVerificationToken: $("#RequestVerificationToken").val(),
            },
            success: function (response) {
                $("#closeEditModalBtn").click();
                if (response.success) {
                    alert(response.message || "Book updated successfully!");
                    $("#editModal").modal("hide");
                    $(".modal-backdrop").remove();

                    if (response.data) {
                        const book = response.data;
                        const authorsNames = Array.isArray(book.authors)
                            ? book.authors.map((author) => author.name || "").join(", ")
                            : "No authors";

                        const row = $(`tr[data-id="${bookId}"]`);
                        row.find(".book-name").text(book.name);
                        row.find(".book-description").text(book.description);
                        row.find(".book-category").text(book.category);
                        row.find(".book-price").text(book.price);
                        row.find(".book-authors").text(authorsNames);
                        row.find(".book-Photos").empty();
                        book.photos.forEach((photo) => {
                            const img = document.createElement("img");
                            img.className = "mr-3";
                            img.src = photo.path;
                            img.alt = photo.name;
                            img.style.maxWidth = "50px";

                            document.querySelector(".book-Photos").appendChild(img);
                        });
                    }
                } else {
                    alert(response.message || "Failed to update the book.");
                }
            },
            error: function (xhr) {
                alert("Error: " + xhr.responseText);
            },
        });
    });

    // Handle Delete
    $(document).on("click", ".btn-delete", function () {  //"#bookTable"
        const row = $(this).closest("tr");
        const bookId = row.data("id");

        if (confirm("Are you sure you want to delete this book?")) {
            $.ajax({
                url: "?handler=Delete&id=" + bookId,
                type: "POST",
                headers: {
                    RequestVerificationToken: $("#RequestVerificationToken").val(),
                },
                success: function (response) {
                    if (response.success) {
                        alert(response.message || "Book deleted successfully!");
                        row.remove();
                    } else {
                        alert(response.message || "Failed to delete the book.");
                    }
                },
                error: function (xhr) {
                    alert("Error: " + xhr.responseText);
                },
            });
        }
    });
});
