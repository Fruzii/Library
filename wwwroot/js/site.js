// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

// $('.datepicker').datepicker();
$('.form__item-drop').on('click', function () {
  $(this).toggleClass('form__item-drop--active');
  $('.search__more').slideToggle(200);
});