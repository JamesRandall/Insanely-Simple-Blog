var InsanelySimpleBlog = { };

InsanelySimpleBlog.start = function (apiEndpoints) {

    Handlebars.registerHelper('date', function (date, block) {
        return moment(date).format('LLL'); //Return a shortened version of the date
    });

    var pageIndex = 0;
    var pageSize = 10;
    var categoryId = null;
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

    var categoryModel = Backbone.Model.extend({
        idAttribute: "CategoryID"
    });
    var categoryCollection = Backbone.Collection.extend({
        model: categoryModel
    });
    var sidebarView = Backbone.View.extend({
        el: sidebarContainer,
        template: Handlebars.compile($("#tpl-sidebar-view").html()),

        initialize: function () {
            this.model.bind("reset", this.render, this);
        },

        render: function () {
            var element = $(this.el);
            var menuModel = {
                Rss: apiEndpoints.rss,
                Atom: apiEndpoints.atom,
                Categories: []
            };
            element.empty();

            _.each(this.model.models, function(row) {
                menuModel.Categories.push(row.toJSON());
            });

            element.html(this.template(menuModel));
        }
    });

    var appRouter = Backbone.Router.extend({
        routes: {
            "posts/:id": "getPost",
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
        },
        defaultRoute: function(actions) {
            this.getPosts();

            $.ajax({
                url: apiEndpoints.getCategories,
                type: 'GET',
                contentType: 'application/json;charset=utf-8'
            }).then(function(categories) {
                var categoriesCollection = new categoryCollection(categories);
                var view = new sidebarView({ model: categoriesCollection });
                view.render();
            });
        }
    });

    
    var appRouterInstance = new appRouter();
    Backbone.history.start();

    return InsanelySimpleBlog;
};