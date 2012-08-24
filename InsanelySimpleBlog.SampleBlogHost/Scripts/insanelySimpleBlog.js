var InsanelySimpleBlog = { };

InsanelySimpleBlog.start = function (apiEndpoints) {
    Handlebars.registerHelper('indexLabel', function(date, block) {
        return moment(date).format('MMMM YYYY');
    });

    Handlebars.registerHelper('date', function (date, block) {
        return moment(date).format('LLL'); //Return a shortened version of the date
    });

    Handlebars.registerHelper('indexUrl', function() {
        var indexModel = this;
        return "#/postsInDateRange/0/" + indexModel.StartDate + "/" + indexModel.EndDate;
    });

    var pageIndex = 0;
    var pageSize = 10;
    var categoryId = null;
    var fromStartDate = null;
    var fromEndDate = null;
    var isSideBarBuilt = false;

    var blogContainer = $(".insanelysimpleblog");
    var postContainer = $("<div class='postcontainer'>");
    var sidebarContainer = $("<div class='sidebarcontainer'>");
    blogContainer.append(sidebarContainer);
    blogContainer.append(postContainer);

    var postModel = Backbone.Model.extend({
        idAttribute: "PostID"
    });
    var postCollection = Backbone.Collection.extend({
        model: postModel
    });
    var postView = Backbone.View.extend({
        el: postContainer,

        initialize: function() {
            this.model.bind("reset", this.render, this);
        },

        render: function() {
            var element = $(this.el);
            element.empty();
            _.each(this.model.models, function(row) {
                element.append(new postItemView({ model: row }).render().el);
            });
        }
    });
    var postItemView = Backbone.View.extend({
        tagName: "div",
        className: "post",
        template: Handlebars.compile($("#tpl-post-item-view").html()),

        initialize: function() {

        },

        render: function() {
            var json = this.model.toJSON();
            $(this.el).html(this.template(json));
            return this;
        }
    });

    var sidebarView = Backbone.View.extend({
        el: sidebarContainer,
        template: Handlebars.compile($("#tpl-sidebar-view").html()),

        initialize: function () {
            
        },

        render: function () {
            var element = $(this.el);
            element.empty();
            element.html(this.template(this.model));
        }
    });

    var appRouter = Backbone.Router.extend({
        routes: {
            "posts/:id": "getPost",
            "postsInDateRange/:pageNumber/:startDate/:endDate": "getPostsInDateRange",
            "posts/": "getPosts",
            "categories/:id": "getPostsForCategory",
            "*actions": "defaultRoute" // matches http://example.com/#anything-here
        },
        getPost: function(id) {
            $.ajax({
                url: apiEndpoints.getPost,
                data: { id: id },
                type: 'GET',
                contentType: 'application/json;charset=utf-8'
            }).then(function(post) {
                var postsCollection = new postCollection([post]);
                var postsView = new postView({ model: postsCollection });
                postsView.render();
            });
            this.getSidebar();
        },
        getPostsInDateRange: function (page, startDate, endDate) {
            pageIndex = page;
            categoryId = null;
            fromStartDate = startDate;
            fromEndDate = endDate;
            $.ajax({
                url: apiEndpoints.getPosts,
                data: { pageNumber: pageIndex, pageSize: pageSize, startDate: fromStartDate, endDate: fromEndDate },
                type: 'GET',
                contentType: 'application/json;charset=utf-8'
            }).then(function (posts) {
                var postsCollection = new postCollection(posts);
                var postsView = new postView({ model: postsCollection });
                postsView.render();
            });
            this.getSidebar();
        },
        getPostsForCategory: function (id) {
            pageIndex = 0;
            categoryId = id;
            $.ajax({
                url: apiEndpoints.getPosts,
                data: { pageNumber: pageIndex, pageSize: pageSize, categoryId: categoryId },
                type: 'GET',
                contentType: 'application/json;charset=utf-8'
            }).then(function (posts) {
                var postsCollection = new postCollection(posts);
                var postsView = new postView({ model: postsCollection });
                postsView.render();
            });
            this.getSidebar();
        },
        getPosts: function () {
            $.ajax({
                url: apiEndpoints.getPosts,
                data: { pageNumber: pageIndex, pageSize: pageSize },
                type: 'GET',
                contentType: 'application/json;charset=utf-8'
            }).then(function (posts) {
                var postsCollection = new postCollection(posts);
                var postsView = new postView({ model: postsCollection });
                postsView.render();
            });
            this.getSidebar();
        },
        getSidebar: function () {
            if (!isSideBarBuilt) {
                $.ajax({
                    url: apiEndpoints.getSidebar,
                    type: 'GET',
                    contentType: 'application/json;charset=utf-8'
                }).then(function (startup) {
                    var sidebarModel = {
                        Rss: apiEndpoints.rss,
                        Atom: apiEndpoints.atom,
                        Categories: startup.Categories,
                        Indices: startup.Indices
                    };

                    var view = new sidebarView({ model: sidebarModel });
                    view.render();
                    isSideBarBuilt = true;
                });
            }
        },
        defaultRoute: function (actions) {
            this.getPosts();            
        }
    });

    
    var appRouterInstance = new appRouter();
    Backbone.history.start();

    return InsanelySimpleBlog;
};