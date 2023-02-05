import { createEffect, createSignal } from "solid-js";

export default function RegexModule(props) {
  const [RegEx, setRegEx] = createSignal("");

  createEffect( () => {
    props.setFunctions( (functions) => {
        functions[props.index()] = {
            "function": "regexCheck",
            "options": {
                regex: RegEx()
            }
        }
        return functions;
    })
  });

  return (
    <div class=" p-3 m-3 flex flex-col rounded shadow shadow-lg bg-slate-100">
      <div class="flex">
        <h2 class="text-xl mb-2 font-bold text-gray-700">Regular Expression</h2>
      </div>
      <div class="flex rounded border-2 border-slate-300 ">
        <div class="p-2 px-3 ">
          <span>RegEx</span>
        </div>
        <input onInput={(e) => setRegEx(e.target.value)} type="text" class="block p-2 w-full rounded text-gray-700 font-bold" />
      </div>
    </div>
  );
}
