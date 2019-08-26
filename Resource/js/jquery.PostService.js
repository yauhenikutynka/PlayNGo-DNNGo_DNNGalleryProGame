
(function ($) {
    $.GalleryPostService = { version: '1.0.0' };

    $.fn.GalleryPostService = function (pp_settings) {
        pp_settings = jQuery.extend({
            ModulePath: '',
            ModuleId: 0,
            TabId: 0,
            PortalId: 0,
            PageIndex: 0,
            FirstScreen: 0,
            LoadDisplay: 0,
            GroupID: 0,
            AjaxType: 'AjaxSliders',
            callback: function (Items, Pages, isEnd) { }
        }, pp_settings);

        var cov = function (v1, v2) {
            return v2 ? v2 : v1;
        };
        return this.each(function () {
            var $this = $(this);

            var Return_Items = new Array();

            var pageindex, pagesize, isEnd, moduleid, tabid, portalid, firstscreen, loaddisplay, groupid;

            pageindex = cov(pp_settings.PageIndex, $this.data('pageindex'));
            firstscreen = cov(pp_settings.FirstScreen, $this.data('firstscreen'));
            loaddisplay = cov(pp_settings.LoadDisplay, $this.data('loaddisplay'));
            moduleid = cov(pp_settings.ModuleId, $this.data('moduleid'));
            tabid = cov(pp_settings.TabId, $this.data('tabid'));
            portalid = cov(pp_settings.PortalId, $this.data('portalid'));
            modulepath = cov(pp_settings.ModulePath, $this.data('modulepath'));
            groupid = cov(pp_settings.GroupID, $this.data('groupid'));
            isEnd = cov(false, $this.data('isend'));
            jQuery.getJSON(modulepath + "Resource_Service.aspx?Token=" + pp_settings.AjaxType + "&ModuleId=" + moduleid + "&TabId=" + tabid + "&PortalId=" + portalid, { PageIndex: pageindex + 1, FirstScreen: firstscreen, LoadDisplay: loaddisplay, GroupID: groupid }, function (data) {
                var Pages = 0;
                if (!isEnd || pageindex == 0) {
                    jQuery.each(data, function (i, item) {
                        Pages = item.Pages;
                        Return_Items.push(item);
                    });
                }
                if (Pages > pageindex) {
                    $this.data("pageindex", $this.data("pageindex") + 1);
                }
                if (Pages <= (pageindex + 1)) {
                    isEnd = true;
                    $this.data('isend', true);
                }else{
                    isEnd = false;
					 $this.data('isend', false);
				}

                pp_settings.callback(Return_Items, Pages, isEnd);
            });
        });
    }

})(jQuery);
