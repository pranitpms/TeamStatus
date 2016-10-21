
angular.module('ui-element', [])
    .directive('uiDropdown', function() {
        return{

            restrict: 'AE',
            template: '<ui-select ng-model="vm.selected" on-select="vm.onSelect({model:$model})" search-enabled="true">' +
                        '<ui-select-match placeholder="Pick one...">{{$select.selected.description}}</ui-select-match>' +
                            '<ui-select-choices repeat="item in vm.config | filter: $select.search track by item.code">' +
                             '<span ng-bind-html="item.description | highlight: $select.search"></span>' +
                        '</ui-select-choices>' +
                    '</ui-select>',
            scope : {
                config : '=',
                model: '=',
                onSelect: '&'
            },
            bindToController: true,
            controllerAs: 'vm',
            controller : function() {

                var vm = this;
                var _selected = {};
                //vm.config = config;
                //vm.selected = value;
                

                Object.defineProperties(this, {
                    selected: {
                        get: function() {
                            return _selected;
                        },
                        set: function(val) {
                            _selected = val;
                            vm.model = val;
                        }
                    }
                });
            }
        };
    });