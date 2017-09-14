angular.module("calendar", [])

.controller("calendarController", function ($scope, $location, $http, growl) {
    var vm = this;

    var date = new Date(),
           d = date.getDate(),
           m = date.getMonth(),
           y = date.getFullYear(),
           started,
           categoryClass;

    SuccessMessage = function (value) {
        growl.success("Successfully" + value, { ttl: 2000 });
    }

    ErrorMessage = function (value) {
        growl.error(value, { title: "Error!", ttl: 3000 });
    }

    $scope.AddMonth = function () {
        $("#calendar").fullCalendar('prev');
    }

    $scope.MinusMonth = function () {
        $("#calendar").fullCalendar('next');
    }

    InitCalendar = function () {
        var calendar = $('#calendar').fullCalendar({
            header: {
                left: 'prev,next today',
                center: 'title',
                right: 'month,agendaWeek,agendaDay'
            },
            selectable: true,
            selectHelper: true,
            select: function (start, end, allDay) {
                $("#calendarModal").modal('show');

                started = start;
                ended = end;

                $(".antosubmit").on("click", function () {
                    var title = $("#title").val();
                    if (end) {
                        ended = end;
                    }

                    categoryClass = $("#event_type").val();

                    if (title) {
                        calendar.fullCalendar('renderEvent', {
                            title: title,
                            start: started,
                            end: end,
                            allDay: allDay
                        },
                          true // make the event "stick"
                        );
                    }

                    $('#title').val('');

                    calendar.fullCalendar('unselect');

                    $('.antoclose').click();

                    return false;
                });
            },
            editable: true,
            events: vm.Schedule,
        });
    }

    $scope.Init = function () {
        $http({
            method: "POST",
            url: "/Calendar/GetSched",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            vm.Schedule = data.data.schedule;

            InitCalendar();
        });
    }

    $scope.SaveHoliday = function (value) {
        $http({
            method: "POST",
            url: "/Calendar/SaveHoliday",
            data: { holiday: value }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                SuccessMessage("Successfully Saved");

                $("#eventModal").modal('hide');

                $scope.InitHoliday();
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });
    }

    $scope.InitHoliday = function () {
        $http({
            method: "POST",
            url: "/Calendar/GetHoliday",
            arguments: { "Content-Type": "application/json" }
        }).then(function (data) {
            if (data.data.errorMessage == "") {
                vm.Holiday = data.data.holiday;

                $scope.ClearModal();
            }
            else {
                ErrorMessage(data.data.errorMessage);
            }
        });
    }

    $scope.ClearModal = function () {
        vm.Modal.title = "";

        vm.Modal.start = {};

        vm.Modal.end = {};
    }

    $scope.AssignModel = function (value) {
        vm.Modal = value;

        if (value.ShowStart != "") {
            vm.Modal.start = new Date(value.ShowStart);
        }

        if (value.ShowEnd != "") {
            vm.Modal.end = new Date(value.ShowEnd);
        }
    }

    $scope.DeleteEvent = function () {
        $http({
            method: "POST",
            url: "/Calendar/DeleteHoliday",
            data: { holiday : vm.Modal }
        }).then(function (data) {
            if (data.data.errorMessage != "") {
                ErrorMessage(data.data.errorMessage);
            }
            else {
                SuccessMessage("Successfully deleted");

                vm.Modal.Status = "X";
            }
        })
    }
});