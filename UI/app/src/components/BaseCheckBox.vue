<template>
    <div class="cukcuk-checkbox" :style="styleCheckBox">
        <input type="button" :class="[bindCheckBoxStatus == '1' ? normalChecked : normalUnchecked]"
        :id="lableName"
        @click="onClickCheckBox"
        @mouseenter="onHoverCheckBox"
        @mouseleave="offHoverCheckBox">
        <label v-if="hasLable" for="" style="margin-left: 4px">{{lableName}}</label>
    </div>
</template>
<script>
export default {
    name: 'BaseCheckBox',
    props: {
        hasLable: Boolean,
        lableName: String,
        styleCheckBox: Object,
        checkBoxValue: String,
    },
    data() {
        return {
            normalUnchecked: "sprite-nor-unchecked",
            hoverUnchecked: "sprite-hover-unchecked",
            normalChecked: "sprite-nor-checked",
            hoverChecked: "sprite-hover-checked",
        }
    },
    computed: {
        bindCheckBoxStatus() {
            return this.checkBoxValue;
        },
    },
    methods: {
        onClickCheckBox(event) {
            if(event.target.className == this.hoverUnchecked || event.target.className == this.normalUnchecked) {
                event.target.className = this.hoverChecked;
                event.target.text = '1';
            } else if(event.target.className == this.hoverChecked || event.target.className == this.normalChecked){
                event.target.className = this.hoverUnchecked;
                event.target.text = '0';
            }
            this.$emit('clickEmitValue', event.target);
        },

        onHoverCheckBox(event) {
            if(event.target.className == this.normalChecked) {
                event.target.className = this.hoverChecked;
            } else if(event.target.className == this.normalUnchecked) {
                event.target.className = this.hoverUnchecked;
            }
        },

        offHoverCheckBox(event) {
            if(event.target.className == this.hoverChecked) {
                event.target.className = this.normalChecked;
            } else if(event.target.className == this.hoverUnchecked) {
                event.target.className = this.normalUnchecked;
            }
        },
    },
}
</script>
<style lang="css" scoped>
    .cukcuk-checkbox {
        display: flex;
        width: fit-content;
        align-items: center;
    }
</style>