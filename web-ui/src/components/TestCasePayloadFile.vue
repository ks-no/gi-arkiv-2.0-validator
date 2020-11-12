<template>
  <PayloadFile
    :fileName="fileName"
    :content="payloadFileContent"
    v-on:get-content="isTextContent => getContent(isTextContent)"
  />
</template>

<script>
import PayloadFile from "./PayloadFile.vue";
import axios from "axios";

export default {
  name: "testCasePayloadFile",

  components: {
    PayloadFile
  },

  data() {
    return {
      payloadFileContent: null,
      fileExtension: null
    };
  },

  props: {
    testName: {
      required: true,
      type: String
    },
    fileName: {
      required: true,
      type: String
    },
    isAttachment: {
      required: false,
      type: Boolean
    }
  },
  
  methods: {
    getContent: function(isTextContent) {
      var endPointUrl = this.isAttachment
        ? this.testName + "/" + this.fileName
        : this.testName;

      var settings = {
        responseType: isTextContent ? "text" : "blob",
        responseEncoding: isTextContent ? "utf-16" : "base64"
      };

      var apiUrl = "/api/TestCasePayloadFiles";
      var resourceUrl = apiUrl + "/" + endPointUrl;
      axios.get(resourceUrl, settings).then(response => {
        this.payloadFileContent = response.data;
      });
    }
  }
};
</script>
