/// <reference path="../../Scripts/angular.min.js" />

angular.module('ui-element',[])
    .directive('UiDatepicker', function () {

        return {
            restrict: 'AE',
            template: '<p class="input-group">' +
                '<input type="text" ng-model="vm.SelectedDate" class="form-control" uib-datepicker-popup="{{vm.Format}}" is-open="vm.opened" min-date="vm.MinDate" max-date="vm.MaxDate" datepicker-options="vm.DateOptions" date-disabled="vm.disabled(date, mode)" ng-required="{{vm.required}}" close-text="Close" />' +
                '<span class="input-group-btn">' +
                '<button type="button" class="btn btn-default" ng-click="vm.open($event)"><i class="fa fa-calendar"></i></button>' +
                '</span>' +
                '</p>',
            bindToController: true,
            controllerAs: 'vm',
            scope: {
                config: '=',
                model: '=',
                disabled: '&',
                onDateSelected: '&'
            },
            controller: function () {

                var vm = this;
                vm.opened = false;
                vm.MinDate = null;
                vm.SelectedDate = null;

                var format = scope.config.dateFormat;
                var minDate = scope.config.minDate;
                var maxDate = scope.config.maxDate;
                var dateOptions = scope.config.dateOptions;
                vm.required = scope.config.required;
                var date = null;

                Object.defineProperties(this, {
                    Format: {
                        get : function() {
                            return format;
                        }
                    },
                    MinDate: {
                        get : function() {
                            return minDate ? new Date(2015, 1, 1) : minDate;
                        }
                    },
                    MaxDate: {
                        get : function() {
                            return maxDate ? new Date(2022, 1, 1) : maxDate;
                        }
                    },
                    DateOptions: {
                        get : function() {
                            return dateOptions ? null : dateOptions;
                        }
                    },
                    SelectedDate: {
                        get : function() {
                            return date;
                        },
                        set : function(val) {
                            date = val;
                            vm.model = val;
                        }
                    }

                });

                vm.disabled = function (date, mode) {
                    if (mode === 'day') {
                        return ((date.getDay() === 0 || date.getDay() === 6) || (date.getDate() > new Date().getDate()));
                    }
                    return (date.getDate() > new Date().getDate());
                }

                vm.open = function ($event) {
                    return vm.opened = true;
                };
           }
        };
    });