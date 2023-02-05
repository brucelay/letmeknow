import { createEffect, createSignal } from "solid-js";

export default function TextModule(props) {
  const [number, setNumber] = createSignal("");

  createEffect( () => {
    props.setFunctions( (functions) => {
        functions[props.index()].options = {
            number: number()
        }
        return functions;
    })
  });

  return (
    <div class="outline rounded p-3 m-3 flex flex-col outline-gray-400">
      <div class="flex">
        <h2 class="text-xl mb-2 font-bold text-gray-700">Text</h2>
      </div>
      <div class="flex rounded border-2 border-slate-200">
        <div class="p-2 px-3 ">
          <span>Number</span>
        </div>
        <input onInput={(e) => setNumber(e.target.value)} type="text" class="rounded block p-2 w-full text-gray-700 font-bold" />
      </div>
    </div>
  );
}
