import App from "@/App";
import TestSession from "@/components/TestSession";
import NewTestSession from "@/components/NewTestSession";

import Vue from "vue";
import Router from "vue-router";

Vue.use(Router);

export default new Router({
  routes: [
    {
      path: "/",
      name: "App",
      component: App
    },
    {
      path: "/TestSession/:testSessionId",
      name: "testSession",
      component: TestSession
    },
    {
      path: "/NewTestSession",
      name: "newTestSession",
      component: NewTestSession
    }
  ]
});
