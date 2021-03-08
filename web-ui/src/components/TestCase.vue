<template>
  <div>
    <span class="grow-right" :class="!hasRun ? 'notHasrun' : ''">
      <span
        v-on:click="isNotCollapsed = !isNotCollapsed"
        v-on:keyup.enter="isNotCollapsed = !isNotCollapsed"
        v-on:keyup.space="isNotCollapsed = !isNotCollapsed"
        v-b-toggle="'collapse-' + testId"
      >
        <div>
          <h5 class="flex-title">
            <b-icon-chevron-right
              v-show="!isNotCollapsed"
              class="expand-icon"
            />
            <b-icon-chevron-down v-show="isNotCollapsed" class="expand-icon" />
            <span> {{ testName }}</span>
            <b-icon-exclamation-circle-fill
              v-if="validState === 'invalid'"
              :class="'validState ' + validState"
              title="Ugyldig"
            />
            <b-icon-check-circle-fill
              v-else-if="validState === 'valid'"
              :class="'validState ' + validState"
              title="Gyldig"
            />
            <b-icon-exclamation-circle-fill
              v-else-if="validState === 'notValidated'"
              :class="'validState ' + validState"
              title="Ikke validert"
            />
          </h5>
        </div>
      </span>
      <b-form-checkbox
        v-if="!hasRun"
        class="ext-left"
        switch
        size="lg"
        :value="testId"
      />
    </span>
    <b-collapse :visible="!isCollapsed" :id="'collapse-' + testId">
      <b-container fluid>
        <b-row style="margin-bottom: 5px">
          <b-col cols="2">
            <strong class="header">Beskrivelse:</strong>
          </b-col>
          <b-col> {{ description }} </b-col>
        </b-row>
        <b-row style="margin-bottom: 5px">
          <b-col cols="2">
            <strong class="header">Testtrinn:</strong>
          </b-col>
          <b-col> {{ testStep }} </b-col>
        </b-row>
        <b-row style="margin-bottom: 5px">
          <b-col cols="2">
            <strong class="header">Operasjon:</strong>
          </b-col>
          <b-col> {{ operation }} </b-col>
        </b-row>
        <b-row style="margin-bottom: 5px">
          <b-col cols="2">
            <strong class="header">Situasjon:</strong>
          </b-col>
          <b-col> {{ situation }} </b-col>
        </b-row>
        <b-row style="margin-bottom: 5px">
          <b-col cols="2">
            <strong class="header">Meldingstype:</strong>
          </b-col>
          <b-col> {{ messageType }} </b-col>
        </b-row>
        <b-row style="margin-bottom: 5px">
          <b-col cols="2"> <strong class="header"> Meldingsinnhold: </strong> </b-col>
          <b-col>
            <TestCasePayloadFile
              :testName="testName"
              :fileName="payloadFileName"
              :operation="operation"
              :situation="situation"
            />
          </b-col>
        </b-row>
        <div v-if="payloadAttachmentFileNames">
          <b-row style="margin-bottom: 5px">
            <b-col cols="2"> <strong class="header">Vedlegg:</strong> </b-col>
            <b-col align-self="stretch">
              <div v-for="attachmentFileName in payloadAttachmentFileNames.split(';')"
                :key="attachmentFileName">
              <TestCasePayloadFile
                :operation="operation"
                :situation="situation"
                :fileName="attachmentFileName"
                :isAttachment="true"
              />
              </div>
            </b-col>
          </b-row>
        </div>
      </b-container>
    </b-collapse>
  </div>
</template>

<script>
import TestCasePayloadFile from "./TestCasePayloadFile.vue";

export default {
  name: "testCase",

  components: {
    TestCasePayloadFile 
  },

  data() {
    return {
      isNotCollapsed: !this.isCollapsed,
      payloadFileContent: null,
      payloadFileContentIsLoaded: false
    };
  },

  props: {
    testId: {
      required: true,
      type: Number
    },
    testName: {
      required: true,
      type: String
    },
    messageType: {
      required: true,
      type: String
    },
    description: {
      required: true,
      type: String
    },
    testStep: {
      required: true,
      type: String
    },
    operation: {
      required: true,
      type: String
    },
    expectedResult: {
      required: true,
      type: String
    },
    situation: {
      required: true,
      type: String
    },
    payloadFileName: {
      required: true,
      type: String
    },
    payloadAttachmentFileNames: {
      type: String
    },
    hasRun: {
      type: Boolean
    },
    validState: {
      type: String
    },
    isCollapsed: {
      required: true,
      type: Boolean
    }
  }
};
</script>

<style scoped>
strong.header {
  float: right;
}

.flex-title {
  display: flex;
  justify-content: space-between;
  width: 100%;
  align-items: center;
}

.flex-title > * {
  display: block;
}

.flex-title span {
  flex: 1;
}

.expand-icon {
  margin-right: 6px;
}

svg.validState {
  font-size: 24px;
}

svg.valid {
  color: green;
}

svg.invalid {
  color: #cc3333;
}

svg.notValidated {
  color: rgb(231, 181, 42);
}

.ext-right {
  float: right;
}

.grow-right {
  width: 100%;
}

.grow-right.notHasrun > span,
.grow-right.notHasrun > div {
  display: inline-block;
  vertical-align: middle;
  margin-left: 3px;
}

div span {
  display: block;
  line-height: 36px;
}

b-form-checkbox.ext-left {
  float: left;
}
</style>
