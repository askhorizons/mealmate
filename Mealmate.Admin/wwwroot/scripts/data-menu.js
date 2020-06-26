"use strict";
var KTData = function () {

    var initTable1 = function () {
        var table = $('#kt_datatable');

        // begin first table
        table.DataTable({
            responsive: true,

            // DOM Layout settings
            dom: 'f' + `<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,

            lengthMenu: [5, 10, 25, 50],

            pageLength: 10,
            pagingType: 'full_numbers',
            searching: true,
            ordering: true,
            language: {
                'lengthMenu': 'Display _MENU_',
            },

            // Order settings
            order: [[1, 'desc']],

            headerCallback: function (thead, data, start, end, display) {
                thead.getElementsByTagName('th')[0].innerHTML = `
                    <label class="checkbox checkbox-single">
                        <input type="checkbox" value="" class="group-checkable"/>
                        <span></span>
                    </label>`;
            },

            columnDefs: [
                {
                    targets: 0,
                    width: '30px',
                    className: 'dt-left',
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return `
                        <label class="checkbox checkbox-single">
                            <input type="checkbox" value="" class="checkable"/>
                            <span></span>
                        </label>`;
                    },
                }
            ],
        });

        table.on('change', '.group-checkable', function () {
            var set = $(this).closest('table').find('td:first-child .checkable');
            var checked = $(this).is(':checked');

            $(set).each(function () {
                if (checked) {
                    $(this).prop('checked', true);
                    $(this).closest('tr').addClass('active');
                }
                else {
                    $(this).prop('checked', false);
                    $(this).closest('tr').removeClass('active');
                }
            });
        });

        table.on('change', 'tbody tr .checkbox', function () {
            $(this).parents('tr').toggleClass('active');
        });
    };

    return {
        //main function to initiate the module
        init: function () {
            initTable1();
        }
    };
}();

jQuery(document).ready(function () {
    KTData.init();
});

function loadMenus(menuTypes, ingredients) {
    $.ajax({
        url: '/Admin/Menu/Detail/',
        type: 'GET',
        data: { MenuTypes: menuTypes, Ingredients: ingredients },
        success: function (response) {
            console.log(response);
            $("#menu-data").html(response);
            KTData.init();
        },
        error: function (err) {
            console.log(err);
        }
    });
}
