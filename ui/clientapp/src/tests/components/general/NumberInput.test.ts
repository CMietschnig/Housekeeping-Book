import NumberInputVue from "@/components/general/NumberInput.vue";
import { mount } from "@vue/test-utils";
import { describe, it ,expect } from "vitest";


describe("NumberInput.vue", () => {
    it("should not have input field, save and cancel buttons", () => {
        const props = {
            id: 1,
            number: 3,
            placeholder: "placeholder",
        };

        //act
        const sut = mount(NumberInputVue, {
            props: props,
        });

        const inputField = sut.find('#id-1');
        const editBtn = sut.find('.edit-btn');
        const deleteBtn = sut.find('.delete-btn');
        const saveBtn = sut.find('.save-btn');
        const resetBtn = sut.find('.reset-btn');


        expect(inputField.exists()).toBe(false);
        expect(editBtn.exists()).toBe(true);
        expect(deleteBtn.exists()).toBe(true);
        expect(saveBtn.exists()).toBe(false);
        expect(resetBtn.exists()).toBe(false);
    })
})