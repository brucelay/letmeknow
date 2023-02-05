import { createSignal } from "solid-js";

export default function ComponentSelect(props) {
  const addFunction = (func) => {
    console.log(func);
    const [options, setOptions] = createSignal({});
    props.setFunctions([...props.functions, { ...func}]);
  };

  return (
    <>
      <For  each={props.modules()}>
        {(module) => (
            <div class="m-3">
                <button className="bg-gray-800 hover:bg-gray-600 text-white font-bold py-2 px-4 rounded"
                        onClick={() => addFunction(module)}>{module.function}</button>
            </div>
        )}
      </For>
    </>
  );
}
