//Select2
//Select2 Multi Append
$.fn.select2.defaults.set("theme", "bootstrap");
var placeholder = "Select...";
$(".select2-single").select2(
  {
    placeholder: placeholder,
    width: null,
    minimumResultsForSearch: 7,
    containerCssClass: ':all:',
  });
$(".select2-multiple").select2(
  {
    placeholder: placeholder,
    width: null,
    containerCssClass: ':all:',
    closeOnSelect: false
  });
$(".select2-allow-clear").select2(
  {
    allowClear: true,
    placeholder: placeholder,
    width: null,
    containerCssClass: ':all:'
  });

// Notes - Duplicate fields
$(function () {
  $(document).on('click', '.btn-add', function (e) {
    e.preventDefault();
    var controlForm = $('.controls form:first'),
      currentEntry = $(this).parents('.entry:first'),
      newEntry = $(currentEntry.clone()).appendTo(controlForm);
    newEntry.find('input').val('');
    controlForm.find('.entry:not(:last) .btn-add').removeClass('btn-add').addClass('btn-remove').removeClass('btn-success').addClass('btn-default').html('<span class="fa fa-times"></span>');
  }).on('click', '.btn-remove', function (e) {
    $(this).parents('.entry:first').remove();
    e.preventDefault();
    return false;
  });
});
// Datapicker
$(function () {
  $('input[name="datefilter"]').daterangepicker(
    {
      autoUpdateInput: false,
      locale:
      {
        cancelLabel: 'Clear'
      }
    });
  $('input[name="datefilter"]').on('apply.daterangepicker', function (ev, picker) {
    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
  });
  $('input[name="datefilter"]').on('cancel.daterangepicker', function (ev, picker) {
    $(this).val('');
  });
});
// Dropdown stop exit onclick
$('.dropdown-menu.no-close').on('click', function (event) {
  var events = $._data(document, 'events') ||
    {};
  events = events.click || [];
  for (var i = 0; i < events.length; i++) {
    if (events[i].selector) {
      //Check if the clicked element matches the event selector
      if ($(event.target).is(events[i].selector)) {
        events[i].handler.call(event.target, event);
      }
      // Check if any of the clicked element parents matches the
      // delegated event selector (Emulating propagation)
      $(event.target).parents(events[i].selector).each(function () {
        events[i].handler.call(this, event);
      });
    }
  }
  event.stopPropagation(); //Always stop propagation
});
// Collapse Button Toggle Text
$('[data-toggle-secondary]').each(function () {
  var $toggle = $(this);
  var originalText = $toggle.text();
  var secondaryText = $toggle.data('toggle-secondary');
  var $target = $($toggle.attr('href'));
  $target.on('show.bs.collapse hide.bs.collapse', function () {
    if ($toggle.text() == originalText) {
      $toggle.text(secondaryText);
    }
    else {
      $toggle.text(originalText);
    }
  });
});
//Switches
$('.custom-switch').each(function () {
  var checkbox = $(this).children('input[type=checkbox]');
  var toggle = $(this).children('label.custom-control-label');

  if (checkbox.length) {
    checkbox.addClass('hidden');
    toggle.removeClass('hidden');
    if (checkbox[0].checked) {
      toggle.addClass('on');
      toggle.removeClass('off');
      toggle.text(toggle.attr('data-on'));
    } else {
      toggle.addClass('off');
      toggle.removeClass('on');
      toggle.text(toggle.attr('data-off'));
    };
  }
});

$('.custom-control-label').click(function () {
  var checkbox = $(this).siblings('input[type=checkbox]')[0];
  var toggle = $(this);

  if (checkbox.checked) {
    toggle.addClass('off');
    toggle.removeClass('on');
    toggle.text(toggle.attr('data-off'));
  } else {
    toggle.addClass('on');
    toggle.removeClass('off');
    toggle.text(toggle.attr('data-on'));
  };
});
// Tooltip
$(function () {
  $('[data-toggle="tooltip"]').tooltip();
});
// Browse
$(document).on('click', '.browse', function () {
  var file = $(this).parent().parent().parent().find('.file');
  file.trigger('click');
});
$(document).on('change', '.file', function () {
  $(this).parent().find('.form-control').val($(this).val().replace(/C:\\fakepath\\/i, ''));
});
// SVG
$(function () {
  $('#Svg-Wrapper').load("./SVG.HTML");
});
// Close notifications
$('.close-icon').on('click',function() {
  $(this).closest('.card-body').slideUp();
  })
// Get current year for footer.
var currentYear = new Date().getFullYear();
$('#current-year').html(currentYear);