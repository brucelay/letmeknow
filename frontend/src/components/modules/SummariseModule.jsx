import { createEffect, createSignal } from "solid-js";

export default function SummariseModule(props) {
  const [maxTokens, setMaxTokens] = createSignal("200");

  createEffect( () => {
    props.setFunctions( (functions) => {
        functions[props.index()].options = {
            maxtokens: maxTokens()
        }
        return functions;
    })
  });

  return (
    <div class=" p-3 m-3 flex flex-col rounded shadow shadow-lg bg-slate-100">
      <div class="flex">
        <h2 class="text-xl mb-2 font-bold text-gray-700">Summarise</h2>
      </div>
      <div class="flex rounded border-2 border-slate-300 ">
        <div class="p-2 px-3 ">
          <span>Characters</span>
        </div>
        <input onInput={(e) => setMaxTokens(e.target.value)} type="text" class="block p-2 w-full rounded text-gray-700 font-bold" />
      </div>
    </div>
  );
}
